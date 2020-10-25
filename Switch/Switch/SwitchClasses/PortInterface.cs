using SharpPcap.Npcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.WinPcap;
using PacketDotNet;
using Switch;
using System.Windows.Forms;

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
        private int port;

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

        public PortInterface(NpcapDevice dev_in, NpcapDevice dev_out, MultilayerSwitch multi_switch, Form1 gui_interface, int port_num)
        {
            myself = dev_in;
            forward_device = dev_out;
            this.multi_switch = multi_switch;
            gui = gui_interface;
            port = port_num;
            //vytvorenie handlera na prichadzajuce packety
            myself.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

            String mac = "";

            //counter
            if (packet is EthernetPacket)
            {
                eth_in++;
                var eth = ((EthernetPacket)packet);
                mac = eth.SourceHardwareAddress.ToString();
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
                        /*if (tcp.DestinationPort == 80 || tcp.DestinationPort == 80)
                            Http_in++;*/
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

            
            multi_switch.CheckMACinTable(mac, port);



            //skontrolujem 
            multi_switch.UpdateStats();

            
          
            //preposlatie na druhy port v pripade ze sa tam nachadza zariadenie.
            //forward_device.SendPacket(packet);
            //multi_switch.ForwardPacket(this, packet);

            /*try
            {
                //MultilayerSwitch.device[1].SendPacket(packet);
                if (packet is EthernetPacket)
                {
                    Eth_out++;
                    var eth = ((EthernetPacket)packet);
                    var ipv4 = eth.Extract<PacketDotNet.IPv4Packet>();
                    var arp = eth.Extract<PacketDotNet.ArpPacket>();
                    if (ipv4 != null)
                    {
                        Ipv4_out++;
                        var tcp = eth.Extract<PacketDotNet.TcpPacket>();
                        var udp = eth.Extract<PacketDotNet.UdpPacket>();
                        var icmp = eth.Extract<PacketDotNet.IcmpV4Packet>();
                        if (tcp != null)
                        {
                            Tcp_out++;
                            if (tcp.DestinationPort == 80 || tcp.DestinationPort == 80)
                                Http_in++;
                        }
                        if (udp != null)
                            Udp_out++;
                        if (icmp != null)
                            Icmp_out++;
                    }
                    if (arp != null)
                    {
                        Arp_out++;
                    }
                }
            }
            catch (Exception except)
            {
                gui.MessageBox.Show(String.Format("Nepodarilo sa odoslat packet" + except.Message), "Confirm");
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
