using System.Net;
using System.Net.Sockets;
using System.Text;

namespace network;

class Program
{
    static void Main(string[] args)
    {
        Server("Hello");
    }

    
    public static void Server(string name)
    {
        UdpClient udpClient = new UdpClient(12345);
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
        Console.WriteLine("Сервер ждёт сообщения от клиента");
        

        
        while (true)
        {
            Thread threadEx = new Thread(Ex);
            threadEx.Start();

            byte[] buffer = udpClient.Receive(ref ipEndPoint);
            var messageText = Encoding.UTF8.GetString(buffer);
            Message? message = Message.DeserializeFromJson(messageText);
            message?.Print();
            if (message?.Text?.ToLower() == "exit") Environment.Exit(0);
            Thread thread = new Thread(SendReply);
            thread.Start();

            void SendReply()
            {
                // отправка ответа от сервера  
                Console.WriteLine("Отправлено ответное сообщение клиенту");
                byte[] data = "Сообщение получено"u8.ToArray();
                udpClient.Send(data, data.Length, ipEndPoint);
                
            }

            void Ex()
            {
                if (Console.ReadKey().Key > 0) Environment.Exit(0);

            }
        }
    }
}