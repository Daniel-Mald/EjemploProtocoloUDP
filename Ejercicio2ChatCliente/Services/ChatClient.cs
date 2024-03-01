using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2ChatCliente.Services
{
    public class ChatClient
    {
        TcpClient client = null!;
        public void Conectar(IPAddress ip)
        {
            IPEndPoint ipe = new IPEndPoint(ip, 9000);
            client = new();
            client.Connect(ipe);

        }
        public void Desconectar()
        {

        }
        private void RecibirMensaje()
        {

        }
        public void EnviarMensaje()
        {

        }
    }
}
