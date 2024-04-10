using MensajeCliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MensajeCliente.Services
{
    public class MensajesService
    {
        public async void EnviarMensaje(MensajesDTO mensaje, ServerModel server)
        {
            var url = $"http://{server?.IP}:7002/mensajitos/?texto={mensaje.Texto}&colorletra={mensaje.ColorLetra}&colorfondo={mensaje.ColorFondo}";
            HttpClient cliente = new();
             await cliente.GetStringAsync(url);
        }
    }
}
