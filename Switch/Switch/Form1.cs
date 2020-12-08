using System;
using System.Drawing;
using System.Windows.Forms;
using SharpPcap;
using SharpPcap.Npcap;
using Switch.SwitchClasses;

namespace Switch
{
    public partial class Form1 : Form
    {
        private CaptureDeviceList allDevices;
        public MultilayerSwitch multi_switch;
        private bool selected = false;

        public Form1()
        {
            InitializeComponent();
            multi_switch = new MultilayerSwitch(this);
            SetRuleInputs();
        }

        private void SetRuleInputs()
        {
            textBox_srcMAC.Text = "-";
            textBox_srcIP.Text = "-";
            textBox_dstMAC.Text = "-";
            textBox_dstIP.Text = "-";
            textBox_dstMAC.Text = "-";
            textBox_dstIP.Text = "-";
            textBox_protocol.Text = "-";
            textBox_srcPort.Text = "-";
            textBox_dstPort.Text = "-";
        }

        //Start Capture button
        private void button2_Click(object sender, EventArgs e)
        {
            int optimum_checked_device = 2;
            int checked_devices = checkedListBox_foundDevices.CheckedItems.Count;

            //kontrola poctu zvolenych zariadeni
            if (checked_devices != optimum_checked_device)
            {
                MessageBox.Show("Nezvolili ste spravny pocet zariadeni!\nSkontrolujte ci ste zvolili prave 2 zariadenia?", "Confirm");
                return;
            }

            //priradenie zariadeni na ktorych sa bude pocuvat
            //Ethernet 4 MAC
            multi_switch.device[0] = (NpcapDevice)allDevices[checkedListBox_foundDevices.CheckedIndices[0]];
            //Ethernet 3 MAC
            multi_switch.device[1] = (NpcapDevice)allDevices[checkedListBox_foundDevices.CheckedIndices[1]];

            //vytvorenie instancii portov a priradenie do Multiswitchu
            multi_switch.portInterfaces[0] = new PortInterface(multi_switch.device[0], multi_switch.device[1], multi_switch, this, 0);
            multi_switch.portInterfaces[1] = new PortInterface(multi_switch.device[1], multi_switch.device[0], multi_switch, this, 1);

            multi_switch.StartCapture();
        }

        //Find devices button
        private void button_findDevices_Click(object sender, EventArgs e)
        {
            checkedListBox_foundDevices.Items.Clear();
            allDevices = CaptureDeviceList.Instance;
            if (allDevices.Count < 1)
            {
                MessageBox.Show("Nenasli sa ziadne zariadenia na pocuvanie premavky!", "Confirm");
                return;
            }

            int i = 0;

            // Vypisanie zariadeni
            foreach (var dev in allDevices)
            {
                checkedListBox_foundDevices.Items.Add(String.Format("{0}", dev.Description), false);
                i++;
            }
        }

        //Button StopCapture
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                multi_switch.StopCapture();
            }
            catch(Exception except)
            {
                MessageBox.Show("Ziadne zariadenie nepocuva, takze sa neda vypnut!", "Confirm");
            }

        }

        //vypisanie CAM tabulky, to by bolo v pripade ze sa to vola z inej triedy
        public string CamTableUpdate
        {
            set { richTextBox2.Text = value; }
        }

        //vypissanie CAM tabulky
        public void PrintCamTable()
        {
            if (richTextBox2.InvokeRequired)
            {
                richTextBox2.BeginInvoke(new MethodInvoker(() => richTextBox2.Clear()));
                richTextBox2.BeginInvoke(new MethodInvoker(() => richTextBox2.AppendText(String.Format("MAC address\tPort\tTimer\n"))));
                foreach (CamTableRecord record in multi_switch.camTable)
                {
                    richTextBox2.BeginInvoke(new MethodInvoker(() => richTextBox2.AppendText(String.Format("{0}\t{1}\t{2}\n", record.mac_addr, record.port_num, record.time_stamp))));
                }
            }
            else
            {
                richTextBox2.Clear();
                richTextBox2.AppendText(String.Format("MAC address\tPort\tTimer\n"));
                try
                {
                    foreach (CamTableRecord record in multi_switch.camTable)
                    {
                        richTextBox2.AppendText(String.Format("{0}\t{1}\t{2}\n", record.mac_addr, record.port_num, record.time_stamp));
                    }
                }
                catch (Exception except)
                {
                    MessageBox.Show("Vyskytla sa interna chyba v CAM tabulke!", "Confirm");
                }
            }
        }

        //vypisanie statistik, to by bolo v pripade ze sa to vola z inej triedy
        public string StatisticsUpdate
        {
            set { richTextBox1.Text = value; }
        }

        //vypisanie statistik
        public void PrintStats()
        {
            int i = 0;
            richTextBox1.Clear();
            richTextBox1.AppendText(String.Format("Port num. IN/OUT\tEth II\tIPv4\tARP\tICMP\tTCP\tUDP\tHTTP\n"));
            foreach (PortInterface port in multi_switch.portInterfaces)
            {
                try
                {
                    richTextBox1.AppendText(String.Format("Port{0} IN:\t\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\n", i, port.eth_in, port.ipv4_in, port.arp_in, port.icmp_in, port.tcp_in, port.udp_in, port.http_in));
                }
                catch (Exception except)
                {
                    MessageBox.Show(String.Format("Port {0} IN Exception null pointer reference\n", i), "Confirm");
                }

                try
                {
                    richTextBox1.AppendText(String.Format("Port{0} OUT:\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\n", i, port.eth_out, port.ipv4_out, port.arp_out, port.icmp_out, port.tcp_out, port.udp_out, port.http_out));
                }
                catch (Exception except)
                {
                    MessageBox.Show(String.Format("Port {0} OUT Exception null pointer reference\n", i), "Confirm");
                }
                i++;
            }
        }

        //configuracia casovaca v zaznamov v CAM tabulke
        private void button_setTimer_Click(object sender, EventArgs e)
        {
            decimal sec = numericUpDown_timer.Value;
            if (sec < 5)
            {
                MessageBox.Show("Set timer on more than 5 sec!", "Confirm");
            }
            else
            {
                multi_switch.defTimeStamp = Decimal.ToInt32(sec);
                label4.Text = String.Format("Timer is set on {0} sec", sec);
            }   
           
        }

        //resetovanie CAM tabulky
        private void button_resetCam_Click(object sender, EventArgs e)
        {
            try
            {
                multi_switch.camTable.Clear();
                PrintCamTable();
            }
            catch(Exception except)
            {
                MessageBox.Show("Neda sa resetovat CAM tabulka, ked este nebolo spustene prepinanie premavky", "Confirm");
            }
}

        //Resetovanie statistik na portoch
        private void button_resetStats_Click(object sender, EventArgs e)
        {
            try
            {
                multi_switch.ResetStats();
                PrintStats();
            }
            catch (Exception except)
            {
                MessageBox.Show("Neda sa resetovat statistika, ked este nebolo spustene prepinanie premavky", "Confirm");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void PrintRules()
        {
            richTextBox1.Clear();
            foreach (Rule rule in multi_switch.rules)
            {
                richTextBox1.AppendText(String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n",
                                                    rule.RuleType, rule.Port, rule.InOut, rule.SrcMAC,
                                                    rule.SrcIP, rule.DstMAC, rule.DstIP,
                                                    rule.Protocol, rule.SrcPort, rule.DstPort));
            }
        }

        private void button_createRule_Click(object sender, EventArgs e)
        {
            String ruleType = comboBox_permitDeny.Text;
            String port = comboBox_port.Text;
            String inOut = comboBox_inOut.Text;
            String srcMac = textBox_srcMAC.Text;
            String srcIP = textBox_srcIP.Text;
            String dstMac = textBox_dstMAC.Text;
            String dstIP = textBox_dstIP.Text;
            String protocol = textBox_protocol.Text;
            String srcPort = textBox_srcPort.Text;
            String dstPort = textBox_dstPort.Text;
            multi_switch.CreateRule(ruleType, port, inOut, srcMac, srcIP, dstMac, dstIP, protocol, srcPort, dstPort);
            var row = new String[] {ruleType, port, inOut, srcMac, srcIP, dstMac, dstIP, protocol, srcPort, dstPort};
            var rule = new ListViewItem(row);
            listView_rules.Items.Add(rule);
            SetRuleInputs();
        }

        private void button_editRule_Click(object sender, EventArgs e)
        {
            if(selected == false)
            {
                MessageBox.Show("Select Rule to Edit", "Confirm");
                return;
            }

            try
            {
                int index = listView_rules.Items.IndexOf(listView_rules.SelectedItems[0]);
                String ruleType = comboBox_permitDeny.Text;
                String port = comboBox_port.Text;
                String inOut = comboBox_inOut.Text;
                String srcMac = textBox_srcMAC.Text;
                String srcIP = textBox_srcIP.Text;
                String dstMac = textBox_dstMAC.Text;
                String dstIP = textBox_dstIP.Text;
                String protocol = textBox_protocol.Text;
                String srcPort = textBox_srcPort.Text;
                String dstPort = textBox_dstPort.Text;
                ListViewItem item = new ListViewItem();
                item = listView_rules.SelectedItems[0];
                item.SubItems[0].Text = ruleType;
                item.SubItems[1].Text = port;
                item.SubItems[2].Text = inOut;
                item.SubItems[3].Text = srcMac;
                item.SubItems[4].Text = srcIP;
                item.SubItems[5].Text = dstMac;
                item.SubItems[6].Text = dstIP;
                item.SubItems[7].Text = protocol;
                item.SubItems[8].Text = srcPort;
                item.SubItems[9].Text = dstPort;
                multi_switch.EditRule(index, ruleType, port, inOut, srcMac, srcIP, dstMac, dstIP, protocol, srcPort, dstPort);
                listView_rules.SelectedIndices.Clear();
                selected = false;
                SetRuleInputs();
                //PrintRules();
            }
            catch (Exception except)
            {
                MessageBox.Show("Select Rule to Edit", "Confirm");
            }
        }

        private void button_selectRule_Click(object sender, EventArgs e)
        {
            if (listView_rules.Items.Count > 0)
            {
                try
                {
                    int index = listView_rules.Items.IndexOf(listView_rules.SelectedItems[0]);
                    comboBox_permitDeny.Text = multi_switch.rules[index].RuleType;
                    comboBox_port.Text = multi_switch.rules[index].Port;
                    comboBox_inOut.Text = multi_switch.rules[index].InOut;
                    textBox_srcMAC.Text = multi_switch.rules[index].SrcMAC;
                    textBox_srcIP.Text = multi_switch.rules[index].SrcIP;
                    textBox_dstMAC.Text = multi_switch.rules[index].DstMAC;
                    textBox_dstIP.Text = multi_switch.rules[index].DstIP;
                    textBox_protocol.Text = multi_switch.rules[index].Protocol;
                    textBox_srcPort.Text = multi_switch.rules[index].SrcPort;
                    textBox_dstPort.Text = multi_switch.rules[index].DstPort;
                    selected = true;
                }
                catch (Exception except)
                {
                    MessageBox.Show("Choose Rule to Edit", "Confirm");
                }
            }
            else
            {
                MessageBox.Show("No Rules to Edit", "Confirm");
            }
        }

        private void button_deleteRule_Click(object sender, EventArgs e)
        {
            if(listView_rules.Items.Count > 0)
            {
                try
                {
                    int index = listView_rules.Items.IndexOf(listView_rules.SelectedItems[0]);
                    listView_rules.Items.Remove(listView_rules.SelectedItems[0]);
                    multi_switch.rules.RemoveAt(index);
                    //PrintRules();
                }
                catch (Exception except)
                {
                    MessageBox.Show("Choose rule to Delete", "Confirm");
                }
            }
            else
            {
                MessageBox.Show("No Rules to Delete", "Confirm");
            }
        }

    }

}
