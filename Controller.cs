using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System;

namespace opg_5_tcp_server
{
    public class Controller
    {
        private static List<FootballPlayer> player_catalog = new List<FootballPlayer>()
        {
            new FootballPlayer(1, "Andy", 1000, 30)
        };

        private static NetworkStream network_stream;
        private static StreamReader stream_reader;
        private static StreamWriter stream_writer;
        public static void handle_client(TcpClient socket)
        {
            network_stream = socket.GetStream();
            stream_reader = new StreamReader(network_stream);
            stream_writer = new StreamWriter(network_stream);

            while (true)
            {
                string cmd = stream_reader.ReadLine();
                //  Create an exit route for the client loop
                if (cmd.ToLower() == "end")
                {
                    send_message("user disconnected");
                    Console.WriteLine("user disconnected");
                    break;
                }

                //  Protocol handler
                switch (cmd.ToLower())
                {
                    case "hentalle":
                        string all_json = JsonSerializer.Serialize<List<FootballPlayer>>(player_catalog);
                        send_message(all_json);
                        break;
                    case "hent":
                        int arg = int.Parse(stream_reader.ReadLine());
                        string one_json = JsonSerializer.Serialize(player_catalog.FirstOrDefault(f => f.Id == arg));
                        send_message(one_json);
                        break;
                    case "gem":
                        string obj_arg = stream_reader.ReadLine();
                        FootballPlayer f = JsonSerializer.Deserialize<FootballPlayer>(obj_arg);
                        player_catalog.Add(f);
                        Console.WriteLine("New player added to catalog : " + obj_arg);
                        break;
                    default:
                        send_message("Please send valid commands");
                        break;
                }
            }
        }
        private static void send_message(string msg)
        {
            stream_writer.WriteLine(msg);
            stream_writer.Flush();
        }
    }
}