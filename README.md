# Prepinac

Softverovy prepinac na predmet PSIP_B

Bolo vela problemov, aby som rozbehal gns3 hlavne funkcnost Cloudov s mojimi Ethernet loopback rozhraniami.
Dalej mi nefungoval flag NoCaptureLocal a packety sa mi duplikovali. Nakoniec som musel naistalovat najnovsiu
verziu npcap (https://nmap.org/npcap/#download) a pri instalacii zvolit, aby sa odinstalovala winpcap kniznica.
Tymto sa cely problem vyriesil. Funguje flag NoCaptureLocal, packety sa mi neduplikuju a funguje aj Cloud v
GNS3.
