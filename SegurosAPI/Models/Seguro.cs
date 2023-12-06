using System;
using System.Collections.Generic;

namespace SegurosAPI.Models
{
    public partial class Seguro
    {
        public Seguro()
        {
            SegurosClientes = new HashSet<SegurosCliente>();
        }

        public int Id { get; set; }
        public string NombreSeguro { get; set; } = null!;
        public string CodigoSeguro { get; set; } = null!;
        public decimal SumaAsegurada { get; set; }
        public decimal Prima { get; set; }

        public virtual ICollection<SegurosCliente> SegurosClientes { get; set; }
    }
}
