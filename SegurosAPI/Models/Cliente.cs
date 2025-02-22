﻿using System.Text.Json.Serialization;

namespace SegurosAPI.Models
{
    public partial class Cliente
    {
        public int Id { get; set; }
        public string Cedula { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int Edad { get; set; }

        [JsonIgnore]
        public virtual ICollection<SegurosCliente> SegurosClientes { get; set; } = new HashSet<SegurosCliente>();
    }
}
