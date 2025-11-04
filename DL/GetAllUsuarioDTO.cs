using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class GetAllUsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string? Nombre { get; set; }
        
        public string? ApellidoPaterno { get; set; }

        public string? Departamento { get; set; }

        public string? NombreUsuario { get; set; }

        public string? PasswordHash { get; set; }

        public bool Status { get; set; }

        public int IdRol { get; set; }


        public string? Rol { get; set; }


    }
}
