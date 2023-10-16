using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Seguro.Model;

public partial class Seguroasegurado
{
    public int IdseguroAsegurado { get; set; }

    public int IdSeguro { get; set; }

    public int IdAsegurado { get; set; }

    [JsonIgnore]
    public virtual Asegurado IdAseguradoNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Seguros IdSeguroNavigation { get; set; } = null!;
}
