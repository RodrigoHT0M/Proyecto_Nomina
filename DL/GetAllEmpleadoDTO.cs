using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class GetAllEmpleadoDTO
    {
        public int IdEmpleado { get; set; }

        public string? Nombre { get; set; }

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public string? FechaNacimiento { get; set; }

        public string? RFC { get; set; }

        public string? NSS { get; set; }

        public string? CURP { get; set; }

        public string? Correo { get; set; }

        public string? FechaIngreso { get; set; }

        public decimal SalarioBase { get; set; }

        public int NoFaltas { get; set; }

        public byte[]? Imagen { get; set; }

        public int IdDepartamento{ get; set; }

        public string? NombreDepartamento { get; set; }


    }
}
