using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MensajeServer.Services
{
    public class DiscoveryService
    {
        UdpClient _server = new()
        {
            EnableBroadcast = true
        };
        IPEndPoint destino = new IPEndPoint(IPAddress.Broadcast, 7000);
        Timer timer;
        byte[] buffer;
        public DiscoveryService()
        {
            var usuario = Environment.UserName;
            buffer = Encoding.UTF8.GetBytes(usuario);
            _server.EnableBroadcast = true;

        }
        public void Saludar()
        {
            _server.Send(buffer,buffer.Length,destino);
        }
    }
}
