using System;
using System.Collections.Generic;

namespace Seguro.Model;

public partial class Seguroasegurado
{
    public int IdseguroAsegurado { get; set; }

    public int IdSeguro { get; set; }

    public int IdAsegurado { get; set; }

    public virtual Asegurado IdAseguradoNavigation { get; set; } = null!;

    public virtual Seguros IdSeguroNavigation { get; set; } = null!;
}
