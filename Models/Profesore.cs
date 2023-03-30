using System;
using System.Collections.Generic;

namespace Tutorias_Unphu.Models;

public partial class Profesore
{
    public string Matricula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public string? Rol { get; set; }

    public string? Estatus { get; set; }

    public virtual Estatus? EstatusNavigation { get; set; }

    public virtual ICollection<ProfAsig> ProfAsigs { get; } = new List<ProfAsig>();

    public virtual ICollection<ProfPem> ProfPems { get; } = new List<ProfPem>();

    public virtual Role? RolNavigation { get; set; }

    public virtual ICollection<Tutorium> Tutoria { get; } = new List<Tutorium>();
}
