using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MensajeCliente.Models;
using MensajeCliente.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MensajeCliente.ViewModels
{
    public partial class MensajesViewModel:ObservableObject
    {
        DiscoveryService _discoveryService = new();
        MensajesService _menajeService = new();
        public MensajesDTO Mensajes { get; set; } = new();
        public ServerModel Seleccionado { get; set; } = null!;
        public List<SolidColorBrush> _colores { get; set; } = new();
        public ObservableCollection<ServerModel> _listaServers { get; set; } = new ObservableCollection<ServerModel>();
        public MensajesViewModel()
        {
            foreach (var item in typeof(Brushes).GetProperties())
            {
                _colores.Add((SolidColorBrush)(item.GetValue(null)?? new SolidColorBrush()));
            }
            _discoveryService.ServidorRecibido += _discoveryService_ServidorRecibido;
        }
        //Colores new (typeof(Brushes).GetProperties())

        private void _discoveryService_ServidorRecibido(object? sender, ServerModel e)
        {
            var server = _listaServers.FirstOrDefault(x=>x.Nombre == e.Nombre);

            if(server == null)
            {
                _listaServers.Add(e);
            }
            else
            {
                server.KeepAlive = e.KeepAlive;
            }
            foreach (var s in _listaServers.ToList())
            {
                if((DateTime.Now - s.KeepAlive).TotalSeconds > 30)
                {
                    _listaServers.Remove(s);
                }
            }
            
        }
        [RelayCommand]
        private void Enviar()
        {
            _menajeService.EnviarMensaje(Mensajes, Seleccionado);
        }
    }
}
