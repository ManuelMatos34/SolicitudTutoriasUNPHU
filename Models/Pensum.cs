using System;
using System.Collections.Generic;

namespace Tutorias_Unphu.Models;

public partial class Pensum
{
    public string NombrePemsum { get; set; } = null!;

    public string? Estatus { get; set; }

    public virtual ICollection<Asignatura> Asignaturas { get; } = new List<Asignatura>();

    public virtual Estatus? EstatusNavigation { get; set; }

    public virtual ICollection<ProfPem> ProfPems { get; } = new List<ProfPem>();

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
