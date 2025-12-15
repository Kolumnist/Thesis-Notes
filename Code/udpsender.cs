using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
    internal class UDP_Sender
    {
        public const int PORT = 20777;

        private static EndPoint _endPoint;
        private static UdpClient _udpClient;

        private static void Main(string[] args)
        {
            _udpClient = new UdpClient();
            
            _endPoint = new IPEndPoint(IPAddress.Loopback, PORT);

            for (int i = 0; i < 6000; i++)
            {
                string text = File.ReadAllText("C:\\Users\\collin\\OneDrive - Veigel GmbH + Co KG\\Dokumente\\Thesis\\Thesis Notes\\Code\\test.json");
                Console.WriteLine(text);

                byte[] data = Encoding.ASCII.GetBytes(text);

                _udpClient.Client.SendTo(data, _endPoint);
                Thread.Sleep(5);
            }
            _udpClient.Close();
        }
    }
