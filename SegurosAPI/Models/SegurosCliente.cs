using System;
using System.Collections.Generic;

namespace SegurosAPI.Models
{
    public partial class SegurosCliente
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int SeguroId { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Seguro Seguro { get; set; } = null!;
    }
}
