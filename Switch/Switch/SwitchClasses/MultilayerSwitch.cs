using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpPcap.Npcap;


namespace Switch.SwitchClasses
{
    public class MultilayerSwitch
    {
        //device[0] Ethernet 4, device[1] Ethernet 3
        public static NpcapDevice[] device = new NpcapDevice[2];
        public static CamTableRecord[] camTable = new CamTableRecord[100]; //vs public List<camTableRecord> camTable;
        public static PortInterface[] portInterfaces = new PortInterface[2];
        public static Form1 gui;
        public int Timer = 30;
        
        //
        public MultilayerSwitch(Form1 f1)
        {
            gui = f1;
        }

        //spusti na oboch zariadeniach capture start
        public void StartCapture()
        {
            int readTimeoutMilliseconds = 10;
            device[0].Open(SharpPcap.Npcap.OpenFlags.Promiscuous | SharpPcap.Npcap.OpenFlags.NoCaptureLocal, readTimeoutMilliseconds);
            device[1].Open(SharpPcap.Npcap.OpenFlags.Promiscuous | SharpPcap.Npcap.OpenFlags.NoCaptureLocal, readTimeoutMilliseconds);
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

            //
        }

        //vypisanie zaznamov z cam tabulky
        public void PrintTable(Form1 f)
        {

        }

        //aktualizovanie cam tabulky
        public void UpdateTable(String mac, int port) { }
        
        public void UpdateStats()
        {
            //gui.richTextBox1.BeginInvoke(new MethodInvoker(() => gui.richTextBox1.AppendText("Abrakadabra")));
            if (gui.richTextBox1.InvokeRequired)
                gui.BeginInvoke(new MethodInvoker(() => gui.PrintStats()));
            else
                gui.PrintStats();
        }

        public int ConfTimer { get { return Timer; } set { Timer = value;} }
    }
}
