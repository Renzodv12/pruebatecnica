using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models;

public partial class Contrato
{
    public int ContratoId { get; set; }

    public string Cliente { get; set; } = null!;

    public int TotalCuotas { get; set; }

    public virtual ICollection<Cuotum> Cuota { get; set; } = new List<Cuotum>();
}
