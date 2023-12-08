using SegurosAPI.DTOs;

namespace SegurosAPI.Services.Contrato
{
    public interface ISegurosClientes
    {
        Task<List<SegurosClienteDto>> GetSegurosClientesList();
        Task<SegurosClienteDto?> GetSegurosCliente(int idSegurosCliente);
        Task<SegurosClienteDto> AddSegurosCliente(CrearAseguradoDto modelo);
        Task<bool> UpdateSegurosCliente(CrearAseguradoDto modelo, int id);
        Task<bool> DeleteSegurosCliente(int id);
    }
}
