using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {

        public int IdUsuario { get; set; }

        public ML.Empleado? Empleado { get; set; }

        public string? Nombre { get; set; }

        public string? PasswordHash { get; set; }

        public bool Status { get; set; }

        public ML.Rol? Rol { get; set; }

        public List<object>? Usuarios { get; set; }
    }
}
