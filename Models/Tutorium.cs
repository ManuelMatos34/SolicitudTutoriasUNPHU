using System;
using System.Collections.Generic;

namespace Tutorias_Unphu.Models;

public partial class Tutorium
{
    public int Id { get; set; }

    public string? IdUsuario { get; set; }

    public string? Asignatura { get; set; }

    public string? IdProfesor { get; set; }

    public string? Horario { get; set; }

    public string? Estatus { get; set; }

    public virtual Asignatura? AsignaturaNavigation { get; set; }

    public virtual Estatus? EstatusNavigation { get; set; }

    public virtual Profesore? IdProfesorNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
