using Ejercicio1ServidorTalleres.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
//using static System.Net.Mime.MediaTypeNames;

namespace Ejercicio1ServidorTalleres.Services
{
    public class InscripcionesServer
    {
        
        public InscripcionesServer()
        {
            var hilo =  new Thread(new ThreadStart(iniciar));
            hilo.IsBackground = true;
            hilo.Start();
        }
        public event EventHandler<InscripcionDTO>? InscripcionRealizada;
        void iniciar()
        {
            UdpClient server = new UdpClient(5001);
            while(true) 
            {
                IPEndPoint remoto = new(IPAddress.Any, 5001);
                byte[] buffer = server.Receive(ref remoto);

                InscripcionDTO? dto = JsonSerializer.Deserialize<InscripcionDTO>(
                    Encoding.UTF8.GetString(buffer)
                    );

                if (dto != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        InscripcionRealizada?.Invoke(this, dto);

                    });
                }
            }
            
        }

    }
}
