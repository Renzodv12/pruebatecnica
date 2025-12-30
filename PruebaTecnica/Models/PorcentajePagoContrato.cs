namespace PruebaTecnica.Models
{
    public class PorcentajePagoContrato
    {
        public int ContratoId { get; set; }
        public int TotalCuotas { get; set; }
        public int CuotasPagadas { get; set; }
        public decimal PorcentajePagado { get; set; }
    }
}
                