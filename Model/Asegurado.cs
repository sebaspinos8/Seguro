using System;
using System.Collections.Generic;

namespace Seguro.Model;

public partial class Asegurado
{
    public int IdAsegurado { get; set; }

    public string CedulaAsegurado { get; set; } = null!;

    public string NombreAsegurado { get; set; } = null!;

    public int TelefonoAsegurado { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public virtual ICollection<Seguroasegurado> Seguroasegurados { get; set; } = new List<Seguroasegurado>();
}
