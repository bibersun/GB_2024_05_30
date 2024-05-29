using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace network;

public class Message
{
    public string? Text { get; set; }
    public DateTime DateTime1 { get; set; }
    public string? NickNameFrom { get; set; }
    public string? NickNameTo { get; set; }

    public string SerializeMessageToJson() => JsonSerializer.Serialize(this);

    public static Message? DeserializeFromJson(string message) => JsonSerializer.Deserialize<Message>(message);

    public void Print()
    {
        Console.WriteLine(this.ToString());
    }

    public override string ToString()
    {
        return $"{this.DateTime1} получено {this.Text} от {this.NickNameFrom}";
    }
    
}