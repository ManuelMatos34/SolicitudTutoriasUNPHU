using System;
using System.Collections.Generic;

namespace Tutorias_Unphu.Models;

public partial class ProfPem
{
    public int Id { get; set; }

    public string? Profesor { get; set; }

    public string? Pemsum { get; set; }

    public string? Estatus { get; set; }

    public virtual Estatus? EstatusNavigation { get; set; }

    public virtual Pensum? PemsumNavigation { get; set; }

    public virtual Profesore? ProfesorNavigation { get; set; }
}
