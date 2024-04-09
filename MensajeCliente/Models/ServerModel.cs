using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MensajeCliente.Models
{
    public class ServerModel
    {
        public IPEndPoint? IP { get; set; }
        public DateTime KeepAlive { get; set; }
        public string Nombre { get; set; } = "";
    }
}
