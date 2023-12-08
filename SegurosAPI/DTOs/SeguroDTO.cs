namespace SegurosAPI.DTOs
{
    public class SeguroDto
    {
        internal int Id;
        public string NombreSeguro { get; set; } = null!;
        public string CodigoSeguro { get; set; } = null!;
        public decimal SumaAsegurada { get; set; }
        public decimal Prima { get; set; }
    }
}
