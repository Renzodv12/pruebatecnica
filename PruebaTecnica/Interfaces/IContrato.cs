using PruebaTecnica.Models;

namespace PruebaTecnica.Interfaces
{
    public interface IContrato
    {
        Task<List<Contrato>> GetAllContratosAsync();
        Task<PorcentajePagoContrato?> ObtenerPorcentajePagoAsync(int contratoId);
        Task<Contrato?> GetByIdAsync(int id);
        Task<Contrato> CreateAsync(Contrato contrato);
        Task<bool> UpdateAsync(Contrato contrato);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
