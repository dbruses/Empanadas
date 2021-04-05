using System;
using System.Collections.Generic;

#nullable disable

namespace EmpanadasWebApi.Models
{
    public partial class EstadosPedido
    {
        public EstadosPedido()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdEstado { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
