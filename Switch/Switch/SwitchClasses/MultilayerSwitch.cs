using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using PacketDotNet.Ieee80211;
using SharpPcap.LibPcap;
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
        public void ForwardPacket(NpcapDevice device, int port, Packet packet)
        {
            bool forward = true;

            try
            {
                Monitor.Enter(buffer);
                try
                {
                    for (int j = 0; j < buffer.Count; j++)
                    {
                        if (packet == buffer[j])
                        {
                            gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port: {0} Zhoda neposielam \n", port))));
                            //buffer.RemoveAt(j);
                            forward = false;
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(buffer);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                Console.WriteLine("A SynchronizationLockException occurred. Message:");
                Console.WriteLine(SyncEx.Message);
            }

            if (forward)
            {
                //zvys statitiky na device out
                gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port: {0} Posielam \n", port))));
                device.SendPacket(packet);

                //Statistics Port OUT
                if (packet is EthernetPacket)
                {
                    portInterfaces[port].eth_in++;
                    var eth = ((EthernetPacket)packet);
                    var ipv4 = eth.Extract<PacketDotNet.IPv4Packet>();
                    var arp = eth.Extract<PacketDotNet.ArpPacket>();
                    if (ipv4 != null)
                    {
                        portInterfaces[port].ipv4_out++;
                        var tcp = eth.Extract<PacketDotNet.TcpPacket>();
                        var udp = eth.Extract<PacketDotNet.UdpPacket>();
                        var icmp = eth.Extract<PacketDotNet.IcmpV4Packet>();
                        if (tcp != null)
                        {
                            portInterfaces[port].tcp_out++;
                            /*if (tcp.DestinationPort == 80 || tcp.DestinationPort == 80)
                                Http_in++;*/
                        }

                        if (udp != null)
                            portInterfaces[port].udp_out++;
                        if (icmp != null)
                            portInterfaces[port].icmp_out++;
                    }
                    if (arp != null)
                    {
                        portInterfaces[port].arp_out++;
                    }
                }
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
