using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet.Ieee80211;
using SharpPcap.Npcap;


namespace Switch.SwitchClasses
{
    public class MultilayerSwitch
    {
        //device[0] Ethernet 4, device[1] Ethernet 3
        public NpcapDevice[] device = new NpcapDevice[2];
        public List<CamTableRecord> camTable = new List<CamTableRecord>();
        public PortInterface[] portInterfaces = new PortInterface[2];
        public Form1 gui;
        public int defTimeStamp = 30;
        
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
        }

        public void StopCapture()
        {
            device[0].Close();
            device[1].Close();
        }

        //tato metoda bude preposielat komunikaciu
        //skontroluje ci dst MAC != src MAC
        //skontroluje ci ma dst MAC v tabulke ak hej tak to preposle na konkretny port
        //Ak nie tak preposle na vsetky okrem toho z kade prisiel
        public void ForwardPacket(PortInterface src_port, PacketDotNet.Packet packet)
        {
            //zisti ci packet sa zachytil na porte, kde sa nachadza aj cielove zariadenie
            //portInterfaces[0].myself.Equals(camTable[i].port_num);

            /*if(src_port.myself.Equals(device[1]))
            {

            }*/
            //
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


        public int ConfTimer { get { return defTimeStamp; } set { defTimeStamp = value;} }

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


        //zisti v ije MAC adresa v tabulke a true or false
        public bool IsMACInTable(String mac)
        {
            CamTableRecord record = camTable.Find(rec => rec.mac_addr.Equals(mac));
            if (record == null)
                return false;
            else
                return true;
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
                record.time_stamp = 30;
            }
            else
            {
                camTable.Add(new CamTableRecord(mac, port, defTimeStamp));
            }

            if (gui.richTextBox2.InvokeRequired)
            {
                gui.BeginInvoke(new MethodInvoker(() => gui.PrintCamTable()));
                //gui.BeginInvoke(new MethodInvoker(() => gui.PrintStats()));
            }
            
            else
            {
                gui.PrintCamTable();
                //gui.PrintStats();
            }
                
        }
    }
}
