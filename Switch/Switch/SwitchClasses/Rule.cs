using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Switch.SwitchClasses
{
    public class Rule
    {
        public String RuleType { get; set; }
        public String Port { get; set; }
        public String InOut { get; set; }
        public String SrcMAC { get; set; }
        public String SrcIP { get; set; }
        public String DstMAC { get; set; }
        public String DstIP { get; set; }
        public String Protocol { get; set; }
        public String SrcPort { get; set; }
        public String DstPort { get; set; }

        public Rule(String ruleType, String port, String inOut, String srcMac, String srcIP, String dstMac, String dstIP, String protocol, String srcPort, String dstPort)
        {
            this.RuleType = ruleType;
            this.Port = port;
            this.InOut = inOut;
            this.SrcMAC = srcMac;
            this.SrcIP = srcIP;
            this.DstMAC = dstMac;
            this.DstIP = dstIP;
            this.Protocol = protocol;
            this.SrcPort = srcPort;
            this.DstPort = dstPort;
        }
    }
}
