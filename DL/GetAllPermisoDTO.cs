using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class GetAllPermisoDTO
    {
        public int IdPermiso { get; set; }

        public int IdEmpleado { get; set; }

        public string? Nombre { get; set; }

        public string? ApellidoPaterno { get; set; }

        public string? Correo { get; set; }

        public string? Departamento { get; set; }

        public string? FechaSolicitud { get; set; }

        public string? FechaInicio { get; set; }

        public string? FechaFin { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public string? Motivo { get; set; }

        public int IdStatusPermiso { get; set; }


        public string? Estatus { get; set; }

        public int? Autorizo { get; set; }

        public string? EmpleadoAutorizo { get; set; }

        public string? ApellidoPaternoAutorizo { get; set; }

        public string? CorreoAutorizo { get; set; }

    }
}
