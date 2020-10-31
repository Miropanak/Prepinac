using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PacketDotNet;
using SharpPcap;
using SharpPcap.Npcap;


namespace Switch.SwitchClasses
{
    public class MultilayerSwitch
    {
        //device[0] Ethernet 4, device[1] Ethernet 3
        public NpcapDevice[] device = new NpcapDevice[2];
        public List<CamTableRecord> camTable = new List<CamTableRecord>();
        public PortInterface[] portInterfaces = new PortInterface[2];
        public List<Packet> buffer = new List<Packet>();
        public static List<CaptureEventArgs> SentPackets = new List<CaptureEventArgs>();
        private static object Lock = new object();
        public Form1 gui;
        public int defTimeStamp = 30;
        private Thread timer;
        
        //
        public MultilayerSwitch(Form1 f1)
        {
            gui = f1;
        }

        //spusti na oboch zariadeniach capture start
        public void StartCapture()
        {
            //int readTimeoutMilliseconds = 10;
            device[0].Open(OpenFlags.Promiscuous | OpenFlags.NoCaptureLocal, 10);
            device[1].Open(OpenFlags.Promiscuous | OpenFlags.NoCaptureLocal, 10);
            device[0].StartCapture();
            device[1].StartCapture();

            //tread fo update statistics and CAM table
            timer = new Thread(UpdateCAMTimer);
            timer.Start();
        }

        public void StopCapture()
        {
            device[0].Close();
            device[1].Close();
            timer.Abort();
        }

        public void UpdateCAMTimer()
        {
            while (true)
            {
                for (int i = 0; i < camTable.Count; i++)
                {
                    camTable[i].time_stamp--;
                    if (camTable[i].time_stamp == 0)
                        camTable.RemoveAt(i);
                }
                gui.BeginInvoke(new MethodInvoker(() => gui.PrintCamTable()));
                gui.BeginInvoke(new MethodInvoker(() => gui.PrintStats()));
                Thread.Sleep(1000);
            }
        }

        public void ResetStats()
        {
            portInterfaces[0].ResetStats();
            portInterfaces[1].ResetStats();
            if (gui.richTextBox2.InvokeRequired)
                gui.BeginInvoke(new MethodInvoker(() => gui.PrintStats()));
            else
                gui.PrintStats();
           
            
        }


        public int ConfTimer { set { defTimeStamp = value;} }

        //zisti na akom porte sa nachadza zariadenie s danou MAC adresou
        //port 0/1 alebo -1 ak sa nenachadza
        public int CheckMACPort(String mac)
        {
            CamTableRecord record = camTable.Find(rec => rec.mac_addr.Equals(mac));
            if (record == null)
                return -1;
            else
                return record.port_num;
        }
        
        public void UpdateCAMTable(String mac, int port)
        {
            CamTableRecord record = camTable.Find(rec => rec.mac_addr.Equals(mac));
            
            if (record != null)
            {
                if(record.port_num != port)
                {
                    record.port_num = port;
                }
                record.time_stamp = defTimeStamp;
            }
            else
            {
                camTable.Add(new CamTableRecord(mac, port, defTimeStamp));
            }   
        }
        public void Set(CaptureEventArgs e)
        {
            lock (Lock)
            {
                SentPackets.Add(e);
            }
        }

        public bool Check(CaptureEventArgs e)
        {
            lock (Lock)
            {
                foreach (var Pk in SentPackets)
                {
                    if (Pk.Packet.Data.SequenceEqual(e.Packet.Data) && Pk.Device == e.Device)// if (Pk.Packet.Data.SequenceEqual(e.Packet.Data) && Pk.Device == e.Device)
                    {
                        SentPackets.Remove(Pk);
                        return true;
                    }
                }
            }
            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("IV Device {0} Duplikat\n", e.Device.MacAddress))));
            return false;
        }
    }
}
