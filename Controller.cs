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

            string cmd = stream_reader.ReadLine();
            System.Console.WriteLine(cmd);
            while (cmd.ToLower() != "end")
            {
                try
                {
                    check_protocol(cmd);
                    cmd = stream_reader.ReadLine();
                    System.Console.WriteLine(cmd);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }

            }
            Console.WriteLine("Closing socket");
            socket.Close();
        }
        private static void send_message(string msg)
        {
            stream_writer.WriteLine(msg);
            stream_writer.Flush();
        }

        private static void check_protocol(string cmd)
        {
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
                    Console.WriteLine(obj_arg);
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
}