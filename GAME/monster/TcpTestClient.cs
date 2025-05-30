using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

public class TcpMonsterClient
{
    public static void Main()
    {
        TcpClient client = new TcpClient("127.0.0.1", 7777);
        NetworkStream stream = client.GetStream();

        var request = new MonsterRequest
        {
            mapId = 1,
            type = "goblin"
        };

        string json = JsonSerializer.Serialize(request);
        byte[] sendData = Encoding.UTF8.GetBytes(json);
        stream.Write(sendData, 0, sendData.Length);

        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        Console.WriteLine("Response from server: " + response);

        client.Close();
    }
} // Note: assumes MonsterRequest & MonsterResponse class exist in scope
