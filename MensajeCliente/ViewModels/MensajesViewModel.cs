using CommunityToolkit.Mvvm.ComponentModel;
using MensajeCliente.Models;
using MensajeCliente.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeCliente.ViewModels
{
    public partial class MensajesViewModel:ObservableObject
    {
        DiscoveryService _discoveryService = new();
        MensajesService _menajeService = new();
        public ObservableCollection<ServerModel> _listaServers { get; set; } = new ObservableCollection<ServerModel>();
        public MensajesViewModel()
        {
            _discoveryService.ServidorRecibido += _discoveryService_ServidorRecibido;
        }

        private void _discoveryService_ServidorRecibido(object? sender, ServerModel e)
        {
            _listaServers.Add(e);
        }
    }
}
