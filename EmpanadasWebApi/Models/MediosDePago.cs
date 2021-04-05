using System;
using System.Collections.Generic;

#nullable disable

namespace EmpanadasWebApi.Models
{
    public partial class MediosDePago
    {
        public MediosDePago()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdMedioPago { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
