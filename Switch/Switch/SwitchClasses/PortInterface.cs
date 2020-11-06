using SharpPcap.Npcap;
using System;
using SharpPcap;
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
        public int http_in;
        public int http_out;

        public PortInterface(NpcapDevice dev_in, NpcapDevice dev_out, MultilayerSwitch multi_switch, Form1 gui_interface, int port_num)
        {
            myself = dev_in;
            forward_device = dev_out;
            this.multi_switch = multi_switch;
            gui = gui_interface;
            device_port = port_num;
            //vytvorenie handlera na prichadzajuce packety
            myself.OnPacketArrival += new PacketArrivalEventHandler(Device_OnPacketArrival);
        }

        private void Device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            //sem zapnut tu kontrolu packetovaj s pridavanim, bez vymazavania,
            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);     

            //skus vypisat
            String src_mac = "";
            String dst_mac = "";

            //Statistics Port IN
            if (packet is EthernetPacket)
            {
                var eth = ((EthernetPacket)packet);
                src_mac = eth.SourceHardwareAddress.ToString();
                dst_mac = eth.DestinationHardwareAddress.ToString();
                UpdateStats(multi_switch.portInterfaces[device_port], packet, "IN");
                if (multi_switch.FilterPacket("IN", device_port, src_mac, dst_mac, packet) == false)
                {
                    gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port {0} IN Nepreposielam\n\n", device_port))));
                    return;
                }
                gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port {0} IN Preposielam\n\n", device_port))));
            }


            //naformatovanie MAC adresy
            src_mac = FormatMAC(src_mac);
            dst_mac = FormatMAC(dst_mac);

            //aktualizovanie CAM tabulky
            multi_switch.UpdateCAMTable(src_mac, device_port);


            //je dst port v tabulke?
            int src_port = multi_switch.CheckMACPort(src_mac);
            int dst_port = multi_switch.CheckMACPort(dst_mac);
          
            //port som nenasiel posielam to na zariadenie, na druhe zariadenie
            int port = ((device_port + 1) % 2);

            //cielove zariadenie sa nachadza na porte z ktoreho prisiel packet, preto neposelam
            if(dst_port == src_port)
            {
                return;
            }
            //cielovy port nepoznam preto posielam na vsetky porty okrem portu ktory tento packet prijal
            else if (dst_port == -1)
            {
                if (multi_switch.FilterPacket("OUT", port, src_mac, dst_mac, packet) == false)
                {
                    gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port {0} OUT Nepreposielam\n\n", port))));
                    return;
                }
                gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port {0} OUT Preposielam\n\n", port))));
                UpdateStats(multi_switch.portInterfaces[port], packet, "OUT");
                forward_device.SendPacket(packet);
            }
            //poznam cielovy port a odlisuje sa od zdrojoveho
            else if (dst_port != src_port)
            {
                if (multi_switch.FilterPacket("OUT", dst_port, src_mac, dst_mac, packet) == false)
                {
                    gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port {0} OUT Nepreposielam\n\n",dst_port))));
                    return;
                }
                gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Port {0} OUT Preposielam\n\n", dst_port))));
                UpdateStats(multi_switch.portInterfaces[dst_port], packet, "OUT");
                forward_device.SendPacket(packet);   
            }
            //sem by som sa uz nemal dostat
            else
            {
               MessageBox.Show("Chyba zaznamu v CAM tabulke", "confirm");
            }
        }

        private void UpdateStats(PortInterface port, Packet packet , String direction)
        {
            if (packet is EthernetPacket && direction == "IN")
            {
                port.eth_in++;
                var eth = ((EthernetPacket)packet);

                var ipv4 = eth.Extract<IPv4Packet>();
                var arp = eth.Extract<ArpPacket>();
                if (ipv4 != null)
                {
                    port.ipv4_in++;
                    var tcp = eth.Extract<TcpPacket>();
                    var udp = eth.Extract<UdpPacket>();
                    var icmp = eth.Extract<IcmpV4Packet>();
                    if (tcp != null)
                    {
                        port.tcp_in++;
                        if (tcp.DestinationPort == 80 || tcp.SourcePort == 80)
                            http_in++;
                    }
                        
                    if (udp != null)
                        port.udp_in++;
                    if (icmp != null)
                        port.icmp_in++;
                }
                if (arp != null)
                {
                    port.arp_in++;
                }
            }

            if (packet is EthernetPacket && direction == "OUT")
            {
                port.eth_out++;
                var eth = ((EthernetPacket)packet);

                var ipv4 = eth.Extract<IPv4Packet>();
                var arp = eth.Extract<ArpPacket>();
                if (ipv4 != null)
                {
                    port.ipv4_out++;
                    var tcp = eth.Extract<TcpPacket>();
                    var udp = eth.Extract<UdpPacket>();
                    var icmp = eth.Extract<IcmpV4Packet>();
                    if (tcp != null)
                    {
                        port.tcp_out++;
                        if (tcp.DestinationPort == 80 || tcp.SourcePort == 80)
                            http_out++;
                    }

                    if (udp != null)
                        port.udp_out++;
                    if (icmp != null)
                        port.icmp_out++;
                }
                if (arp != null)
                {
                    port.arp_out++;
                }
            }

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
            http_in = 0;
            http_out = 0;
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
    }
}
