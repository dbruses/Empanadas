using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMPedidos.Models
{
    public class Pedido
    {
        public long IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreYapellido { get; set; }
        public string Direccion { get; set; }
        public int TotalEmpanadas { get; set; }
        public double ImportePedido { get; set; }
        public int MedioPago { get; set; }
        public int Estado { get; set; }

        public List<DetallePedido> detallePedidos { get; set; }
    }
}
