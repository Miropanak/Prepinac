using SharpPcap.Npcap;
using System;
using System.Collections.Generic;
using SharpPcap;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpPcap.LibPcap;
using SharpPcap.WinPcap;
using PacketDotNet;
using Switch;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Diagnostics;

namespace Switch.SwitchClasses
{
    public class PortInterface
    {
        //aktualny port na ktorom som prijal ramec
        public NpcapDevice myself;
        public NpcapDevice forward_device;
        //port na ktory preposielam komunikaciu
        private MultilayerSwitch multi_switch;
        private Form1 gui;
        private int device_port;

        public int eth_in;
        public int eth_out;
        public int ipv4_in;
        public int ipv4_out;
        public int arp_in;
        public int arp_out;
        public int icmp_in;
        public int icmp_out;
        public int tcp_in;
        public int tcp_out;
        public int udp_in;
        public int udp_out;
        public int counter = 0;

        public PortInterface(NpcapDevice dev_in, NpcapDevice dev_out, MultilayerSwitch multi_switch, Form1 gui_interface, int port_num)
        {
            myself = dev_in;
            forward_device = dev_out;
            this.multi_switch = multi_switch;
            gui = gui_interface;
            device_port = port_num;
            //vytvorenie handlera na prichadzajuce packety
            myself.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            if (MultilayerSwitch.Check(e))
            {
                return;
            }

            //sem zapnut tu kontrolu packetovaj s pridavanim, bez vymazavania,
            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            //bool forward = true;          


            //exist = multi_switch.buffer.Contains(packet);
            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Device_PacketOnArrival {0} \n", counter++))));


            //try
            //{
            /*Monitor.Enter(multi_switch.buffer);
            try
            {
                lock (multi_switch.buffer)
                {
                    for (int j = 0; j < multi_switch.buffer.Count; j++)
                    {
                        if (packet == multi_switch.buffer[j])
                        {
                            gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port: {0} Zhoda neposielam \n", device_port))));
                            forward = false;
                            return;
                        }
                    }
                }
            }
            finally
            {
                Monitor.Exit(multi_switch.buffer);
            }*/
           /* }
            catch (SynchronizationLockException SyncEx)
            {
                Console.WriteLine("A SynchronizationLockException occurred. Message:");
                Console.WriteLine(SyncEx.Message);
            }*/


            /*if (forward)
            {
                multi_switch.buffer.Add(packet);
                lock (multi_switch.buffer)
                {
                    for (int j = 0; j < multi_switch.buffer.Count; j++)
                    {
                        gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Packet {0}\n{1}\n", j, packet.ToString()))));
                    }
                }*/

                //skus vypisat
                String src_mac = "";
                String dst_mac = "";

                //Statistics Port IN
                if (packet is EthernetPacket)
                {
                    eth_in++;
                    var eth = ((EthernetPacket)packet);
                    src_mac = eth.SourceHardwareAddress.ToString();
                    dst_mac = eth.DestinationHardwareAddress.ToString();

                    if (src_mac == "02004C4F4F50" || src_mac == "01005E0000FC" || dst_mac == "02004C4F4F50" || dst_mac == "01005E0000FC")
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port: {0}  srcMAC {1} dstMAC {2} Nepovolena adresa \n", device_port, src_mac, dst_mac))));
                        return;
                    }

                    var ipv4 = eth.Extract<PacketDotNet.IPv4Packet>();
                    var arp = eth.Extract<PacketDotNet.ArpPacket>();
                    if (ipv4 != null)
                    {
                        ipv4_in++;
                        var tcp = eth.Extract<PacketDotNet.TcpPacket>();
                        var udp = eth.Extract<PacketDotNet.UdpPacket>();
                        var icmp = eth.Extract<PacketDotNet.IcmpV4Packet>();
                        if (tcp != null)
                        {
                            tcp_in++;
                            //if (tcp.DestinationPort == 80 || tcp.DestinationPort == 80)
                            //    Http_in++;
                        }

                        if (udp != null)
                            udp_in++;
                        if (icmp != null)
                            icmp_in++;
                    }
                    if (arp != null)
                    {
                        arp_in++;
                    }
                }


                //naformatovanie MAC adresy
                src_mac = FormatMAC(src_mac);
                dst_mac = FormatMAC(dst_mac);

                //aktualizovanie cam tabulky CAM table
                lock (multi_switch.camTable)
                {
                    multi_switch.UpdateCAMTable(src_mac, device_port);
                }


                //je dst port v tabulke?
                int dst_port;
                int src_port;
                lock (multi_switch.camTable)
                {
                    dst_port = multi_switch.CheckMACPort(dst_mac);
                    src_port = multi_switch.CheckMACPort(src_mac);
                }

                //port som nenasiel posielam to na zariadenie, na druhe zariadenie


                int port = ((device_port + 1) % 2);


                if (dst_port == -1)
                {
                    //zvys statistiky
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port: {0} src_port: {1} dst_port {2} srcMAC {3} dstMAC {4} dst nepoznam preposielam cez port {5} \n", device_port, src_port, dst_port, src_mac, dst_mac, dst_port))));
                    //multi_switch.ForwardPacket(forward_device,  port, packet);
                    //forward_device.SendPacket(packet);
                    forward_device.SendPacket(e.Packet.Data);
                    MultilayerSwitch.Set(new CaptureEventArgs(e.Packet, forward_device));
            }
                //zisti dst port
                else if (dst_port != src_port)
                {
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port: {0} src_port: {1} dst_port {2} srcMAC {3} dstMAC {4} dst poznam preposielam cez port {5} \n", device_port, src_port, dst_port, src_mac, dst_mac, dst_port))));
                    //multi_switch.ForwardPacket(forward_device, port, packet);
                    forward_device.SendPacket(e.Packet.Data);
                    MultilayerSwitch.Set(new CaptureEventArgs(e.Packet, forward_device));
            }
                //nerob nic lebo zariadenie tuto spravu uz dostal
                else
                {
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port: {0} src_port: {1} dst_port {2} srcMAC {3} dstMAC {4} nerobim nic\n", device_port, src_port, dst_port, src_mac, dst_mac))));
                }
           /* }
            else
            {
                gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port {0} Packet Exists!!!\n", device_port))));
            }*/
           


            /*bool forward = true;
            gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("List<Packet> {0}\n", multi_switch.buffer.Count))));

            try
            {
                Monitor.Enter(multi_switch.buffer);
                try
                {
                    for (int j = 0; j < multi_switch.buffer.Count; j++)
                    {
                        if (packet == multi_switch.buffer[j])
                        {
                            gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port: {0} Duplikat Neposielam \n", device_port))));
                            forward = false;
                            
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(multi_switch.buffer);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                Console.WriteLine("A SynchronizationLockException occurred. Message:");
                Console.WriteLine(SyncEx.Message);
            }*/
        }

        public void ResetStats()
        {
            eth_in = 0;
            eth_out = 0;
            ipv4_in = 0;
            ipv4_out = 0;
            arp_in = 0;
            arp_out = 0;
            icmp_in = 0;
            icmp_out = 0;
            tcp_in = 0;
            tcp_out = 0;
            udp_in = 0;
            udp_out = 0;
        }
        
        public String FormatMAC(String mac)
        {
            String mac_addr;
            if(mac == "")
            {
                mac_addr = "00:00:00:00:00:00";
            }
            else
            {
                mac_addr = String.Format("{0}{1}:{2}{3}:{4}{5}:{6}{7}:{8}{9}:{10}{11}", mac[0], mac[1], mac[2], mac[3], mac[4], mac[5], mac[6], mac[7], mac[8], mac[9], mac[10], mac[11]);
            }
            return mac_addr;

        }

        public int Eth_in { get; set; }
        public int Eth_out { get; set; }
        public int Ipv4_in { get; set; }
        public int Ipv4_out { get; set; }
        public int Arp_in { get; set; }
        public int Arp_out { get; set; }
        public int Icmp_in { get; set; }
        public int Icmp_out { get; set; }
        public int Tcp_in { get; set; }
        public int Tcp_out { get; set; }
        public int Udp_in { get; set; }
        public int Udp_out { get; set; }
    }
}
