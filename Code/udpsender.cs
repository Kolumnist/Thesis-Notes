
using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        UdpClient _udpClient = new UdpClient(LISTENING_PORT);
        IPEndPoint _endPoint = new IPEndPoint(IPAddress.Any, LISTENING_PORT);

        while (true){
          
            // Store received data from client
            string msg = ConfigurationSettings.AppSettings[studentName];
            if (msg == null) msg = "No such Student available for conversation";
            byte[] data = Encoding.ASCII.GetBytes(msg);
            udpc.Send(data, data.Length, _endPoint);
        }

        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        IPAddress serverAddr = IPAddress.Parse("192.168.2.255");

        EndPoint endPoint = new IPEndPoint(serverAddr, 20777);
        Console.WriteLine("Is On");

        string text = File.ReadAllText("C:/Users/collin/OneDrive - Veigel GmbH + Co KG/Dokumente/Thesis/Thesis Notes/Zyklus_2/test.json");
        Console.WriteLine(text);

        byte[] send_buffer = Encoding.ASCII.GetBytes(text);

        sock.SendTo(send_buffer, endPoint);
    }
}