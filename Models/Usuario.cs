using System;
using System.Collections.Generic;

namespace Tutorias_Unphu.Models;

public partial class Usuario
{
    public string Matricula { get; set; } = null!;

    public string? Password { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public int? Edad { get; set; }

    public string? Pemsum { get; set; }

    public string? Rol { get; set; }

    public string? Estatus { get; set; }

    public virtual Estatus? EstatusNavigation { get; set; }

    public virtual Pensum? PemsumNavigation { get; set; }

    public virtual Role? RolNavigation { get; set; }

    public virtual ICollection<Tutorium> Tutoria { get; } = new List<Tutorium>();
}
