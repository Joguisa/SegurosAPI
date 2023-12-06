using Microsoft.EntityFrameworkCore;
using SegurosAPI.DTOs;
using SegurosAPI.Models;
using SegurosAPI.Services.Contrato;

namespace SegurosAPI.Services.Implementacion
{
    public class SeguroService : ISeguro
    {
        private readonly DBSegurosContext context;

        public SeguroService(DBSegurosContext context)
        {
            this.context = context;
        }


        public async Task<List<SeguroDTO>> GetList()
        {
            try
            {
                var seguros = await context.Seguros.ToListAsync();
                var listaSeguros = seguros.Select(s => new SeguroDTO
                {
                    NombreSeguro = s.NombreSeguro,
                    CodigoSeguro = s.CodigoSeguro,
                    SumaAsegurada = s.SumaAsegurada,
                    Prima = s.Prima

                }).ToList();

                return listaSeguros;

            } catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SeguroDTO?> Get(int idSeguro)
        {
            try
            {
                Seguro? seguro = await context.Seguros
                    .Where(s => s.Id == idSeguro)
                    .FirstOrDefaultAsync();
                return seguro == null
                    ? null
                    : new SeguroDTO
                    {
                        NombreSeguro = seguro.NombreSeguro,
                        CodigoSeguro = seguro.CodigoSeguro,
                        SumaAsegurada = seguro.SumaAsegurada,
                        Prima = seguro.Prima
                    };
            } catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<SeguroDTO> Add(SeguroDTO modelo)
        {
            try
            {
                if ( string.IsNullOrEmpty(modelo.NombreSeguro)
                    || string.IsNullOrEmpty(modelo.CodigoSeguro)
                    || modelo.SumaAsegurada <= 0
                    || modelo.Prima <= 0)
                {
                    throw new ArgumentException("Los campos requeridos deben ser proporcionados.");
                }

                // Verificar si ya existe un cliente con la misma cédula
                if (await SeguroExiste(modelo.CodigoSeguro))
                {
                    throw new InvalidOperationException("Ya existe un seguro con el mismo código.");
                }

                var seguro = new Seguro
                {
                    NombreSeguro = modelo.NombreSeguro,
                    CodigoSeguro = modelo.CodigoSeguro,
                    SumaAsegurada = modelo.SumaAsegurada,
                    Prima = modelo.Prima
                };


                context.Seguros.Add(seguro);
                await context.SaveChangesAsync();

                modelo.id = seguro.Id;

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> SeguroExiste(string codigo)
        {
            return await context.Seguros.AnyAsync(c => c.CodigoSeguro == codigo);
        }

        public async Task<bool> Update(SeguroDTO modelo, int idSeguro)
        {
            try
            {
                
                if (idSeguro <= 0 
                    || string.IsNullOrEmpty(modelo.NombreSeguro) 
                    || string.IsNullOrEmpty(modelo.CodigoSeguro)
                    || modelo.SumaAsegurada <= 0 
                    || modelo.Prima <= 0)
                {
                    throw new ArgumentException("ID y otros campos requeridos deben ser proporcionados.");
                }

                var seguroExistente = await context.Seguros.FindAsync(idSeguro);
                if (seguroExistente == null)
                {
                    throw new ArgumentException($"No se encontró un seguro con el ID: {idSeguro}");
                }

                seguroExistente.NombreSeguro = modelo.NombreSeguro;
                seguroExistente.CodigoSeguro = modelo.CodigoSeguro;
                seguroExistente.SumaAsegurada = modelo.SumaAsegurada;
                seguroExistente.Prima = modelo.Prima;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int idSeguro)
        {
            try
            {
                var seguroExistente = await context.Seguros.FindAsync(idSeguro);
                if (seguroExistente == null)
                {
                    throw new ArgumentException($"No se encontró un cliente con el ID: {idSeguro}");
                }

                context.Seguros.Remove(seguroExistente);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
