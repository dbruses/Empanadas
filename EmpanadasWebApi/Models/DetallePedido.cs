using System;
using System.Collections.Generic;

#nullable disable

namespace EmpanadasWebApi.Models
{
    public partial class DetallePedido
    {
        public long IdDetallePedido { get; set; }
        public long IdPedido { get; set; }
        public int IdGusto { get; set; }
        public int Cantidad { get; set; }
        public double Total { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
