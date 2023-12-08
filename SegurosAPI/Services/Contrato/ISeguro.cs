using SegurosAPI.DTOs;

namespace SegurosAPI.Services.Contrato
{
    public interface ISeguro
    {
        Task<List<SeguroDto>> GetList();
        Task<SeguroDto> Get(int idSeguro);
        Task<SeguroDto> Add(SeguroDto modelo);
        Task<bool> Update(SeguroDto modelo, int idSeguro);
        Task<bool> Delete(int idSeguro);

    }
}
