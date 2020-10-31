using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.Npcap;
using SharpPcap.WinPcap;
using PacketDotNet;
using Switch.SwitchClasses;
using System.Runtime.InteropServices;

namespace Switch
{
    public partial class Form1 : Form
    {
        private CaptureDeviceList allDevices;
        public MultilayerSwitch multi_switch;

        public Form1()
        {
            InitializeComponent();
            multi_switch = new MultilayerSwitch(this);
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
            //Ethernet 4 MAC fe80::bd32:e328:48b3:ee5f%29
            multi_switch.device[0] = (NpcapDevice)allDevices[checkedListBox_foundDevices.CheckedIndices[0]];
            //Ethernet 3 MAC fe80::7d9c:2f68:b092:7993%15
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
                //richTextBox1.AppendText((String.Format("Device number: {0}\n{1} ", i, dev.ToString())));
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
    }

}
