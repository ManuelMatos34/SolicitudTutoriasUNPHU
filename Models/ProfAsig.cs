using System;
using System.Collections.Generic;

namespace Tutorias_Unphu.Models;

public partial class ProfAsig
{
    public int Id { get; set; }

    public string? Prof { get; set; }

    public string? Asig { get; set; }

    public virtual Asignatura? AsigNavigation { get; set; }

    public virtual Profesore? ProfNavigation { get; set; }
}
