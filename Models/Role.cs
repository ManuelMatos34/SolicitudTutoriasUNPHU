using System;
using System.Collections.Generic;

namespace Tutorias_Unphu.Models;

public partial class Role
{
    public string Rol { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Estatus { get; set; }

    public virtual Estatus? EstatusNavigation { get; set; }

    public virtual ICollection<Profesore> Profesores { get; } = new List<Profesore>();

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
