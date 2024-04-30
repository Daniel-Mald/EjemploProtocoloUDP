using MensajeCliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MensajeCliente.Services
{
    public class MensajesService
    {
        public async void EnviarMensaje(MensajesDTO mensaje, ServerModel server)
        {
            var url = $"http://{server.IP?.Address.ToString()}:7002/mensajitos/?texto={mensaje.Texto}&colorletra={HttpUtility.UrlEncode(mensaje.ColorLetra)}&colorfondo={HttpUtility.UrlEncode(mensaje.ColorFondo)}";
            HttpClient cliente = new();
             await cliente.GetStringAsync(url);
        }
    }
}
