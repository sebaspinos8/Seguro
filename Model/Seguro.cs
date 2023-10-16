using System;
using System.Collections.Generic;

namespace Seguro.Model;

public partial class Seguros
{
    public int IdSeguro { get; set; }

    public string NombreSeguro { get; set; } = null!;

    public string CodigoSeguro { get; set; } = null!;

    public decimal SumaAsegurada { get; set; }

    public decimal Prima { get; set; }

    public virtual ICollection<Seguroasegurado> Seguroasegurados { get; set; } = new List<Seguroasegurado>();
}
