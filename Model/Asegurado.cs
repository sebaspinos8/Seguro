using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Seguro.Model;

public partial class Asegurado
{
    public int IdAsegurado { get; set; }

    public string CedulaAsegurado { get; set; } = null!;

    public string NombreAsegurado { get; set; } = null!;

    public int TelefonoAsegurado { get; set; }

    public DateTime FechaNacimiento { get; set; }

    [JsonIgnore]
    public virtual ICollection<Seguroasegurado> Seguroasegurados { get; set; } = new List<Seguroasegurado>();
}
