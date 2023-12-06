namespace SegurosAPI.DTOs
{
    public class SeguroDTO
    {
        public int id;
        public string NombreSeguro { get; set; } = null!;
        public string CodigoSeguro { get; set; } = null!;
        public decimal SumaAsegurada { get; set; }
        public decimal Prima { get; set; }
    }
}
