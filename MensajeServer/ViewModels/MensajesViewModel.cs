using CommunityToolkit.Mvvm.ComponentModel;
using MensajeServer.Models;
using MensajeServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeServer.ViewModels
{
    public partial class MensajesViewModel:ObservableObject
    {
        [ObservableProperty]
        private Mensaje? mensaje;


        MensajesService mensajesService = new();
        DiscoveryService discoveryService = new DiscoveryService();

        public MensajesViewModel()
        {
            mensajesService.Mensaje += Server_MensajeRecibido;
        }

        private void evento()
        {
            if(e.Mensaje != null)
            {
                //Mensaje.ColorLetra = e.ColorLetra;
                //Mensaje.ColorFondo = e.ColorFondo;

                //Mensaje.Texto = e.Cotenedo;
                Mensaje = e;

            }

        }
    }
}
