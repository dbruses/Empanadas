using System;
using System.Collections.Generic;

#nullable disable

namespace EmpanadasWebApi.Models
{
    public partial class Gusto
    {
        public int IdGusto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
    }
}
