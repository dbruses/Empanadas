using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMPedidos.Models
{
    public class DetallePedido
    {
        public long IdDetallePedido { get; set; }
        public long IdPedido { get; set; }
        public int IdGusto { get; set; }
        public int Cantidad { get; set; }
        public double Total { get; set; }
    }
}
