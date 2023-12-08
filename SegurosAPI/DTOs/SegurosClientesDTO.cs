using SegurosAPI.Models;

namespace SegurosAPI.DTOs
{
    public class SegurosClienteDto
    {
        public int Id { get; set; }
        public ClienteDto? Cliente { get; set; } = null!;
        public SeguroDto? Seguro { get; set; } = null!;
    }
}
