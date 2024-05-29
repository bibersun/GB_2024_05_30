using System.Net;
using System.Net.Sockets;
using System.Text;
using network;

namespace Client;

public class Program
{
    static void Main(string[] args)
    {

        SentMessage(args[0], args[1]);


    }

    public static void SentMessage(string from, string ip)
    {

        UdpClient udpClient = new UdpClient();
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);

        while (true)
        {
            string? messageText;
            do
            {
                Console.WriteLine("Введите сообщение");
                messageText = Console.ReadLine();
            } while (string.IsNullOrEmpty(messageText));

            Message message = new Message()
                { Text = messageText, NickNameFrom = from, NickNameTo = "Server", DateTime1 = DateTime.Now };
            string json = message.SerializeMessageToJson();
            byte[] data = Encoding.UTF8.GetBytes(json);
            udpClient.Send(data, data.Length, ipEndPoint);
            Console.WriteLine($"Отправлено сообщение на сервер {messageText}");
            if (messageText.ToLower() == "exit") break;

            var messageR = udpClient.Receive(ref ipEndPoint);
            var messageTextC = Encoding.UTF8.GetString(messageR);
            Console.WriteLine(messageTextC);

        }
    }
}