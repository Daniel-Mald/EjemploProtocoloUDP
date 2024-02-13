// See https://aka.ms/new-console-template for more information

//SERVIDOR UDP
using System.Net;
using System.Net.Sockets;
using System.Text;


//Bind
IPEndPoint endPoint = new(IPAddress.Any, 5001);
UdpClient server = new(endPoint);
server.EnableBroadcast = true;  
while (true)
{
    byte[] buffer = server.Receive(ref endPoint);

    string mensaje = Encoding.UTF8.GetString(buffer);

    Console.WriteLine("Mensaje recibido: " + mensaje);

}


