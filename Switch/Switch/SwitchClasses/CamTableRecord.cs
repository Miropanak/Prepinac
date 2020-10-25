using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.SwitchClasses
{
    public class CamTableRecord
    {
        public String mac_addr;
        public int port_num;
        public int time_stamp;

        public CamTableRecord(String mac, int port, int time)
        {
            mac_addr = mac;
            port_num = port;
            time_stamp = time;
        }

        public string Mac_addr { get; set; }
        public int Port_num { get; set; }
        public int Time_stamp { get; set; }

        //vrati naformatovanu mac s :::
        String Fomat_mac()
        {
            return "";
        }
    }
}
