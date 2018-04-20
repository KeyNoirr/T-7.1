using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RecvBroadcst
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork,
            SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            sock.Bind(iep);
            EndPoint ep = (EndPoint)iep;
            Console.WriteLine("Ready To Receiv...!");
            byte[] data = new byte[1024];
            while (sock.Poll(10000000, SelectMode.SelectRead))
            {
                int recv = sock.ReceiveFrom(data, ref ep);
                sock.SendTo(data, iep);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine("Received: {0} from: {1}", stringData, ep.ToString());
                data = new byte[1024];
                recv = sock.ReceiveFrom(data, ref ep);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine("Received: {0} from: {1}", stringData, ep.ToString());
            }
            sock.Close();
        }
    }
}
