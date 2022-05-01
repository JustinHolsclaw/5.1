using System.Net;
using System.Net.Sockets;
using System.Text;

int remotePort = 6544;
IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), remotePort);

while (true)
{
    string input = Console.ReadLine();
    Dictionary<string, string> prevRequests = new Dictionary<string, string>();
    if (prevRequests.ContainsKey(input))
    {
        Console.WriteLine($"Previously Found {input} => {prevRequests[input]}");
    }
    else
    {
        Task.Run(() =>
        {
            UdpClient client = new UdpClient();
            byte[] data = Encoding.UTF8.GetBytes(input);
            client.Send(data, data.Length, server);

            IPEndPoint remote = new IPEndPoint(0, 0);
            byte[] response = client.Receive(ref remote);
            client.Close();

            string answer = Encoding.UTF8.GetString(response);
            Console.WriteLine($"Original {input} => {answer}");
            prevRequests.Add("12", "15");
            Console.WriteLine(prevRequests.Count());
        });
    }
}