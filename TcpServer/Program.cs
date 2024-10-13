using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TcpServer
{
    class Program
    {
        static void Main()
        {
            StartServer();
        }

        public static void StartServer()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8888;

            TcpListener server = new TcpListener(ipAddress, port);
            server.Start();

            Console.WriteLine("Сервер запущен...");

   
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Kлиент подключен.");

                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string reguest = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Запрос от клиента: { reguest}");

                string response = "";

                if (reguest.ToLower() == "date")
                {
                    response = DateTime.Now.ToShortDateString();
                }
                else if (reguest.ToLower() == "time")
                {
                    response = DateTime.Now.ToShortTimeString();
                }
                else
                {
                    Console.WriteLine("Неправильный запрос");
                }

                byte[] data = Encoding.ASCII.GetBytes(response);
                stream.Write(data, 0, data.Length);

                client.Close();

                Console.ReadLine();
            }
        }
    }
}
