using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models;

public partial class Cuotum
{
    public int CuotaId { get; set; }

    public int ContratoId { get; set; }

    public decimal Monto { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Contrato Contrato { get; set; } = null!;
}
