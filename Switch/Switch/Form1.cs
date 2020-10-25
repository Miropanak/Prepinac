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
            MultilayerSwitch.device[0] = (NpcapDevice)allDevices[checkedListBox_foundDevices.CheckedIndices[0]];
            //Ethernet 3 MAC fe80::7d9c:2f68:b092:7993%15
            MultilayerSwitch.device[1] = (NpcapDevice)allDevices[checkedListBox_foundDevices.CheckedIndices[1]];

            //vytvorenie instancii portov a priradenie do Multiswitchu
            MultilayerSwitch.portInterfaces[0] = new PortInterface(MultilayerSwitch.device[0], multi_switch, this);
            MultilayerSwitch.portInterfaces[1] = new PortInterface(MultilayerSwitch.device[1], multi_switch, this);

            

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
                checkedListBox_foundDevices.Items.Add(String.Format("Device number: {0} {1}", i, dev.Description), false);
                //richTextBox1.AppendText((String.Format("Device number: {0}\n{1} ", i, dev.ToString())));
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

        /*private void device2_OnPacketArrival(object sender, CaptureEventArgs e)
        {

            //nejaky counter a filter na statistiky
            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            if (sender.Equals(MultilayerSwitch.device[1]))
                richTextBox1.BeginInvoke(new MethodInvoker(() => richTextBox1.AppendText(String.Format("tu som Ethernet 3\n"))));
            else
                richTextBox1.BeginInvoke(new MethodInvoker(() => richTextBox1.AppendText(String.Format("tu som Ethernet 4\n"))));
        }*/

        //vypisanie CAM tabulky, to by bolo v pripade ze sa to vola z inej triedy
        public string CamTableUpdate
        {
            set { richTextBox2.Text = value; }
        }

        //vypissanie CAM tabulky
        public void PrintCamTable()
        {
            richTextBox2.Clear();
            richTextBox2.AppendText(String.Format("          MAC address          | Port | Timer \n"));
            try
            {
                foreach (CamTableRecord record in MultilayerSwitch.camTable)
                {
                    richTextBox2.AppendText(String.Format("{0} |  {1}  |  {2}\n", record.mac_addr, record.port_num, record.time_stamp));
                }
            }
            catch (Exception except)
            {
                MessageBox.Show("CAM tabulka je zatial prazdna!", "Confirm");
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
            foreach (PortInterface port in MultilayerSwitch.portInterfaces)
            {
                try
                {
                    richTextBox1.AppendText(String.Format("Port {0} IN : Ethernet II {1} | IPv4 {2} | ARP {3} | ICMP {4} | TCP {5} | UDP {6} \n", i, port.eth_in, port.ipv4_in, port.arp_in, port.icmp_in, port.tcp_in, port.udp_in));
                }
                catch (Exception ex)
                {
                    richTextBox1.AppendText(String.Format("Port {0} IN Exception null pointer reference\n", i));
                }

                try
                {
                    richTextBox1.AppendText(String.Format("Port {0} OUT : Ethernet II {1} | IPv4 {2} | ARP {3} | ICMP {4} | TCP {5} | UDP {6} \n", i, port.eth_out, port.ipv4_out, port.arp_out, port.icmp_out, port.tcp_out, port.udp_out));
                }
                catch (Exception ex)
                {
                    richTextBox1.AppendText(String.Format("Port {0} OUT Exception null pointer reference\n", i));
                }
                i++;
            }
        }

        //configuracia casovaca v zaznamov v CAM tabulke
        private void button_setTimer_Click(object sender, EventArgs e)
        {
            decimal sec = numericUpDown_timer.Value;
            multi_switch.Timer = Decimal.ToInt32(sec);
        }

        //resetovanie CAM tabulky
        private void button_resetCam_Click(object sender, EventArgs e)
        {
            //PrintCamTable(); len som skusal vypisat cam tabulku
            MultilayerSwitch.camTable = new CamTableRecord[100];
        }

        //Resetovanie statistik na portoch
        private void button_resetStats_Click(object sender, EventArgs e)
        {
            PrintStats();
            /*try
            {
                foreach(PortInterface port in MultilayerSwitch.portInterfaces)
                {
                    port.ResetStats();
                }
                /*MultilayerSwitch.portInterfaces[0].ResetStats();
                MultilayerSwitch.portInterfaces[1].ResetStats();*/
            /*}
            catch (Exception except)
            {
                MessageBox.Show("Statistiky su prazdne!", "Confirm");
            }*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
