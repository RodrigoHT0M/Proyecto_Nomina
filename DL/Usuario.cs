using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdEmpleado { get; set; }

    public string? NombreUsuario { get; set; }

    public string? PasswordHash { get; set; }

    public bool? Status { get; set; }

    public int? IdRol { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
