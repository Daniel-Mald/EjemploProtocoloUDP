using Ejercicio2Chat.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ejercicio2Chat.Services
{
    public class ChatServer
    {
        TcpListener server = null!;
        List<TcpClient> clients = new List<TcpClient>();

        public void Iniciar()
        {
            server = new(new IPEndPoint(IPAddress.Any,9000));
            server.Start();
            new Thread(Escuchar) { IsBackground = true }.Start();

        }

        void Escuchar()
        {
            while (server.Server.Connected)
            {
               var tcpClient=  server.AcceptTcpClient();
                clients.Add(tcpClient);

                Thread t = new(() =>
                {
                    RecibirMensajes(tcpClient);
                });
                t.IsBackground = true;
                t.Start();
            }
        }
        void RecibirMensajes(TcpClient cliente)
        {
            while (cliente.Connected)
            {
                var ns = cliente.GetStream();
                while(cliente.Available== 0)
                {
                    Thread.Sleep(500);
                }
                byte[] buffer = new byte[cliente.Available];
                ns.Read(buffer,0, buffer.Length);
                string json = Encoding.UTF8.GetString(buffer);

                var mensaje = JsonSerializer.Deserialize<MensajeDTO>(json);

                if(mensaje != null)
                {
                    if (mensaje.Mensaje == "**HEllO")
                    {

                    }
                    else if (mensaje.Mensaje == "**BYE")
                    {
                        
                    }
                    else
                    {

                    }
                }
            }
            clients.Remove(cliente);
        }
        public void Detener()
        {
            if(server != null) { server.Stop(); }
        }
    }
}
