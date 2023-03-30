using System;
using System.Collections.Generic;

namespace Tutorias_Unphu.Models;

public partial class Asignatura
{
    public string CodigoAsignatura { get; set; } = null!;

    public string? NombreAsignatura { get; set; }

    public string? Estatus { get; set; }

    public string? Pemsum { get; set; }

    public virtual Estatus? EstatusNavigation { get; set; }

    public virtual Pensum? PemsumNavigation { get; set; }

    public virtual ICollection<ProfAsig> ProfAsigs { get; } = new List<ProfAsig>();

    public virtual ICollection<Tutorium> Tutoria { get; } = new List<Tutorium>();
}
