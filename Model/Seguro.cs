using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Seguro.Model;

public partial class Seguros
{
    public int IdSeguro { get; set; }

    public string NombreSeguro { get; set; } = null!;

    public string CodigoSeguro { get; set; } = null!;

    public decimal SumaAsegurada { get; set; }

    public decimal Prima { get; set; }

    [JsonIgnore]
    public virtual ICollection<Seguroasegurado> Seguroasegurados { get; set; } = new List<Seguroasegurado>();
}
