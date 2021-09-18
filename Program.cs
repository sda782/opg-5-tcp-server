using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace opg_5_tcp_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server");
            TcpListener listener = new TcpListener(IPAddress.Any, 2121);
            listener.Start();
            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine("Client connected with ip : " + socket.Client.RemoteEndPoint);
                Task.Run(() =>
                {
                    Controller.handle_client(socket);
                });
            }
        }

    }
}
