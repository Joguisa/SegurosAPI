using SegurosAPI.DTOs;

namespace SegurosAPI.Services.Contrato
{
    public interface ISeguro
    {
        Task<List<SeguroDTO>> GetList();
        Task<SeguroDTO> Get(int idSeguro);
        Task<SeguroDTO> Add(SeguroDTO modelo);
        Task<bool> Update(SeguroDTO modelo, int idSeguro);
        Task<bool> Delete(int idSeguro);

    }
}
