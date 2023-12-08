using SegurosAPI.DTOs;


namespace SegurosAPI.Services.Contrato
{
    public interface ICliente
    {
        Task<List<ClienteDto>> GetList();
        Task<ClienteDto?> Get(int idCliente);
        Task<ClienteDto> Add(ClienteDto modelo);
        Task<bool> Update(ClienteDto modelo, int id);
        Task<bool> Delete(int id);
    }
}
