using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
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
        public List<Rule> rules = new List<Rule>();
        public PortInterface[] portInterfaces = new PortInterface[2];
        public Form1 gui;
        public int defTimeStamp = 30;
        private Thread timer;
        
        //
        public MultilayerSwitch(Form1 f1)
        {
            gui = f1;
        }

        public void CreateRule(String ruleType, String port, String inOut, String srcMac, String srcIP, String dstMac, String dstIP, String protocol, String srcPort, String dstPort)
        {
            rules.Add(new Rule(ruleType, port, inOut, srcMac, srcIP, dstMac, dstIP, protocol, srcPort, dstPort));
        }

        public void EditRule(int index, String ruleType, String port, String inOut, String srcMac, String srcIP, String dstMac, String dstIP, String protocol, String srcPort, String dstPort)
        {
            rules[index].RuleType = ruleType;
            rules[index].Port = port;
            rules[index].InOut = inOut;
            rules[index].SrcMAC = srcMac;
            rules[index].SrcIP = srcIP;
            rules[index].DstMAC = dstMac;
            rules[index].DstIP = dstIP;
            rules[index].Protocol = protocol;
            rules[index].SrcPort = srcPort;
            rules[index].DstPort = dstPort;
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
        
        //aktualizovanie CAM tabulky
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

        public bool FilterPacket(String inOut, int port_num,  String srcMAC, String dstMAC, Packet packet)
        {
            bool Permit;
            int i = 0;
            //prechadzam vsetky pravidla ak najdem pravidla ktore to matchne a je Permit
            //tak return true, ak matchne a je Deny vrati false, ak prejde cely for each
            //a nematchne ziadne pravidlo tak hodi false
            foreach(Rule rule in rules)
            {
                //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Rule {0} {1}\n", i, rule.RuleType))));
                i = i + 1;
                //set rule Permit or Deny
                Permit = TypeControl(rule);
                var eth = ((EthernetPacket)packet);
                
                //Port
                if (rule.Port == "0" && port_num == 1 || rule.Port == "1" && port_num == 0)
                    continue;
                //kontrola in/out
                if (rule.InOut == "IN" && inOut == "OUT" || rule.InOut == "OUT" && inOut == "IN")
                    continue;
                //srcMAC je - alebo rule.SrcMAC == srcMAC tak pokracujem v skumani pravidla
                if (rule.SrcMAC != "-" && rule.SrcMAC != srcMAC)
                {
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("SrcMAC {0} != {1}\n", rule.SrcMAC, srcMAC))));
                    continue;
                }
                //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("SrcMAC {0} == {1}\n", rule.SrcMAC, srcMAC))));

                if (rule.DstMAC != "-" && rule.DstMAC != dstMAC)
                {
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("DstMAC {0} != {1}\n", rule.DstMAC, dstMAC))));
                    continue;
                }
                //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("DstMAC {0} == {1}\n", rule.DstMAC, dstMAC))));

                var ipv4 = eth.Extract<IPv4Packet>();
                var arp = eth.Extract<ArpPacket>();
                if (ipv4 != null)
                {
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText("Som Ipv4 packet\n")));
                    //kontrola IPv4 protokolu
                    if (rule.Protocol != "-" && rule.Protocol != ipv4.Protocol.ToString())
                    {
                       // gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Protocol {0} != {1}\n", rule.Protocol, ipv4.Protocol.ToString()))));
                        continue;
                    }

                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Protocol {0} == {1}\n", rule.Protocol, ipv4.Protocol.ToString()))));

                    //kontrola src a dst IP
                    if (rule.SrcIP != "-" && rule.SrcIP != ipv4.SourceAddress.ToString())
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("SrcIP {0} != {1}\n", rule.SrcIP, ipv4.SourceAddress.ToString()))));
                        continue;
                    }
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("SrcIP {0} == {1}\n", rule.SrcIP, ipv4.SourceAddress.ToString()))));
                    if (rule.DstIP != "-" && rule.DstIP != ipv4.DestinationAddress.ToString())
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("DstIP {0} != {1}\n", rule.DstIP, ipv4.DestinationAddress.ToString()))));
                        continue;
                    }
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("DstIP {0} == {1}\n", rule.DstIP, ipv4.DestinationAddress.ToString()))));

                    var tcp = eth.Extract<TcpPacket>();
                    var udp = eth.Extract<UdpPacket>();
                    var icmp = eth.Extract<IcmpV4Packet>();    
                    
                    if (tcp != null)
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText("Som TCP packet\n")));
                        if (rule.SrcPort != "-" && rule.SrcPort != tcp.SourcePort.ToString())
                        {
                            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Tcp port Src {0} != {1}\n", rule.SrcPort, tcp.SourcePort.ToString()))));
                            continue;
                        }
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Tcp port Src {0} == {1}\n", rule.SrcPort, tcp.SourcePort.ToString()))));
                        if (rule.DstPort != "-" && rule.DstPort != tcp.DestinationPort.ToString())
                        {
                            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Tcp port Dst {0} != {1}\n", rule.DstPort, tcp.DestinationPort.ToString()))));
                            continue;
                        }
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Tcp port Dst {0} == {1}\n", rule.DstPort, tcp.DestinationPort.ToString()))));
                    }

                    if (udp != null)
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText("Som UDP packet\n")));
                        if (rule.SrcPort != "-" && rule.SrcPort != udp.SourcePort.ToString())
                        {
                            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("UDP port Src {0} != {1}\n", rule.SrcPort, udp.SourcePort.ToString()))));
                            continue;
                        }
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("UDP port Src {0} == {1}\n", rule.SrcPort, udp.SourcePort.ToString()))));
                        if (rule.DstPort != "-" && rule.DstPort != udp.DestinationPort.ToString())
                        {
                            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Udp port Dst {0} != {1}\n", rule.DstPort, udp.DestinationPort.ToString()))));
                            continue;
                        }
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Udp port Dst {0} == {1}\n", rule.DstPort, udp.DestinationPort.ToString()))));
                    }

                    if (icmp != null)
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText("Som ICMP packet\n")));
                        if (rule.SrcPort != "-" && rule.SrcPort != icmp.TypeCode.ToString())
                        {
                            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Typ Icmp Src {0} != {1}\n", rule.SrcPort, icmp.TypeCode))));
                            continue;
                        }
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Typ Icmp Src {0} == {1}\n", rule.SrcPort, icmp.TypeCode))));
                        if (rule.DstPort != "-" && rule.DstPort != icmp.TypeCode.ToString())
                        {
                            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Typ Icmp Dst {0} != {1}\n", rule.DstPort, icmp.TypeCode))));
                            continue;
                        }
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Typ Icmp Dst {0} == {1}\n", rule.DstPort, icmp.TypeCode))));
                    }


                    //ak som presiel sem to znamena ze pravidlo preslo cez vsetky kontroly
                    if (Permit)
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Zhoda s pravidlom {0} {1} Preposielam\n", i, rule.RuleType))));
                        return true;
                    }
                    else
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Zhoda s pravidlom {0} {1} Nepreposielam\n", i, rule.RuleType))));
                        return false;  
                    }
                                        
                }
                //arp briem ako protokol protokolu
                if (arp != null)
                {
                    //kontrola ARP
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Som ARP\n"))));
                    if (rule.SrcPort != "-" || rule.DstPort != "-")
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("SrcPort == {0} DstPort == {1} \n", rule.SrcPort, rule.DstPort))));
                        continue;
                    }
                    if (rule.Protocol != "-" && rule.Protocol != "arp")
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Protocol {0} != arp\n", rule.Protocol))));
                        continue;
                    }
                    //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Protocol {0} == arp\n", rule.Protocol))));

                    //ak je to povolovacie pravidlo tak vrat pravdu ze moze presmerovat
                    if (Permit)
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Zhoda s pravidlom {0} {1} Preposielam\n", i, rule.RuleType))));
                        return true;
                    }
                    else
                    {
                        //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Zhoda s pravidlom {0} {1} Nepreposielam\n", i, rule.RuleType))));
                        return false;
                    }
                }

            }

            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText(String.Format("Nenasiel som zhodu {0} Nepreposielam\n", i))));
            return false;
        }

        public bool TypeControl(Rule rule)
        {
            if (rule.RuleType == "Permit")
                return true;
            else
                return false;
        }
    }
}
