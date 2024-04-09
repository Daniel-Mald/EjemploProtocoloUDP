using MensajeCliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MensajeCliente.Services
{
    public class DiscoveryService
    {
        UdpClient clienteEscuchar = new();
        public event EventHandler<ServerModel>? ServidorRecibido;
        public DiscoveryService()
        {
            SolicitarServidores();
            new Thread(RecibirServidores) { IsBackground = true }.Start();
        }
        void SolicitarServidores()//Preguntar que servidores ya estan conectados cuando se ejecute el cliente
        {
            UdpClient _client = new();
            _client.EnableBroadcast = true;
            _client.Send(new byte[] { 1 }, 1, new IPEndPoint(IPAddress.Broadcast, 7001));
            _client.Close();
        }
       
        void RecibirServidores()
        {
            while (true)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
               byte[] buffer =clienteEscuchar.Receive(ref endPoint);
                ServerModel server = new()
                {
                    Nombre = Encoding.UTF8.GetString(buffer),
                    IP = endPoint,
                    KeepAlive = DateTime.Now
                };
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ServidorRecibido?.Invoke(this, server);
                });
            }
        }
    }
}
