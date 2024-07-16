using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PortListener
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("portListener");

            if (args.Length == 0)
            {
                Console.WriteLine("usage: portListener port");
                Console.ReadKey();
                return;
            }



            int port = Convert.ToInt32(args[0]);
            TcpListener listener = new TcpListener(IPAddress.Parse("0.0.0.0"), port);
            listener.Start();

            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);

            while (true)
            {
                string inputLine = "";
                while (inputLine != null)
                {
                    inputLine = reader.ReadLine();
                    writer.WriteLine("Echoing string: " + inputLine);
                    Console.WriteLine("Echoing string: " + inputLine);
                }
                Console.WriteLine("Server saw disconnect from client.");
                Console.ReadLine();
            }


        }
    }
}
