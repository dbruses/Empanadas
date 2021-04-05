using System;
using System.Collections.Generic;

#nullable disable

namespace EmpanadasWebApi.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            DetallePedidos = new HashSet<DetallePedido>();
        }

        public long IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreYapellido { get; set; }
        public string Direccion { get; set; }
        public int TotalEmpanadas { get; set; }
        public double ImportePedido { get; set; }
        public int MedioPago { get; set; }
        public int Estado { get; set; }

        public virtual EstadosPedido EstadoNavigation { get; set; }
        public virtual MediosDePago MedioPagoNavigation { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
