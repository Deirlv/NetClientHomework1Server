using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

namespace NetClientHomework1Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.178.34");

            IPEndPoint endPoint = new IPEndPoint(ipAddress, 8080);

            Socket pass_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            pass_socket.Bind(endPoint);
            pass_socket.Listen(10);

            Console.WriteLine($"Server started at port {endPoint.Port}");

            try
            {
                while (true)
                {
                    Socket ns = pass_socket.Accept();

                    Console.WriteLine($"Client {ns.LocalEndPoint} was connected");
                    Console.WriteLine($"Client {ns.RemoteEndPoint} was connected");

                    byte[] buffer = new byte[1024];
                    int len = ns.Receive(buffer);
                    Console.WriteLine(Encoding.Default.GetString(buffer, 0, len));

                    ns.Send(Encoding.Default.GetBytes($"Server {ns.LocalEndPoint} sent answer in {DateTime.Now}\n"));

                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
