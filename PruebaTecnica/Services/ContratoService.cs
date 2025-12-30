using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Models;
using System.Data;

namespace PruebaTecnica.Interfaces
{
    public class ContratoService(PruebaTecnicaDbContext _context, IConfiguration _configuration) : IContrato
    {
        public async Task<List<Contrato>> GetAllContratosAsync()
        {
            // Si tu DbSet se llama Contrato (singular) por use-database-names:
            return await _context.Contratos
                .AsNoTracking()
                .OrderByDescending(x => x.ContratoId)
                .ToListAsync();
        }

        public async Task<Contrato?> GetByIdAsync(int id)
        {
            return await _context.Contratos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ContratoId == id);
        }

        public async Task<Contrato> CreateAsync(Contrato contrato)
        {
            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();
            return contrato;
        }

        public async Task<bool> UpdateAsync(Contrato contrato)
        {
            // Opcional: verificar existencia antes
            var exists = await _context.Contratos.AnyAsync(x => x.ContratoId == contrato.ContratoId);
            if (!exists) return false;

            _context.Contratos.Update(contrato);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Contratos.FirstOrDefaultAsync(x => x.ContratoId == id);
            if (entity is null) return false;

            _context.Contratos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Contratos.AnyAsync(x => x.ContratoId == id);
        }
        public async Task<PorcentajePagoContrato?> ObtenerPorcentajePagoAsync(int contratoId)
        {
            using var connection = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            using var command = new SqlCommand(
                "sp_CalcularPorcentajePagoContrato", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ContratoId", contratoId);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return new PorcentajePagoContrato
            {
                ContratoId = reader.GetInt32(reader.GetOrdinal("ContratoId")),
                TotalCuotas = reader.GetInt32(reader.GetOrdinal("TotalCuotas")),
                CuotasPagadas = reader.GetInt32(reader.GetOrdinal("CuotasPagadas")),
                PorcentajePagado = reader.GetDecimal(reader.GetOrdinal("PorcentajePagado"))
            };
        }
    }
}
