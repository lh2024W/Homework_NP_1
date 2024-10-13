using System.Net.Sockets;
using System.Text;

namespace Homework_NP_1
{
    public class Program
    {
        static void Main()
        {
            string servarAddress = "127.0.0.1";
            int port = 8888;
        
            try
            {
                TcpClient client = new TcpClient(servarAddress, port);
                Console.WriteLine("Подключение к серверу...");

                NetworkStream stream = client.GetStream();
                Console.WriteLine("Введите 'date', чтоб получить дату или 'time', чтоб получить время:");
                string reguest = Console.ReadLine();

                byte[] data = Encoding.ASCII.GetBytes(reguest);
                stream.Write(data, 0, data.Length);

                data = new byte[256];
                StringBuilder response = new StringBuilder();
                int bytes = stream.Read(data, 0, data.Length);
                response.Append(Encoding.ASCII.GetString(data, 0, bytes));
                Console.WriteLine($"Ответ сервера: {response}");

                
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
