using System;
using System.Collections.Generic;

namespace Tutorias_Unphu.Models;

public partial class Estatus
{
    public string Estatus1 { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Asignatura> Asignaturas { get; } = new List<Asignatura>();

    public virtual ICollection<Pensum> Pensums { get; } = new List<Pensum>();

    public virtual ICollection<ProfPem> ProfPems { get; } = new List<ProfPem>();

    public virtual ICollection<Profesore> Profesores { get; } = new List<Profesore>();

    public virtual ICollection<Role> Roles { get; } = new List<Role>();

    public virtual ICollection<Tutorium> Tutoria { get; } = new List<Tutorium>();

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
