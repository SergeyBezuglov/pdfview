
using Newtonsoft.Json;
using PIMS.Application;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PIMS.Infrastructure
{
    public class Server
    {
        private TcpListener tcpListener;

        public Server(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(ip, port);
        }

        public void Start()
        {
            tcpListener.Start();
            Console.WriteLine("Server started...");

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                var stream = client.GetStream();
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);

                var word = reader.ReadLine();
                var results = new BooleanSearchEngine("pdfs").Search(word);
                var json = JsonConvert.SerializeObject(results, Formatting.Indented);
                writer.WriteLine(json);
                writer.Flush();
            }
        }
    }
}
