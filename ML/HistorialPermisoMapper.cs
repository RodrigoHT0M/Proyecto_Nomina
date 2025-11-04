using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class HistorialPermisoMapper
    {
        public static ML.HistorialPermiso Map(DL.GetAllHistorialPermisoDTO dto)
        {
            ML.HistorialPermiso historialPermiso = new ML.HistorialPermiso();
            historialPermiso.IdHistorialPermiso = dto.IdHistorialPermiso;
            historialPermiso.FechaRevision = dto.FechaRevision.ToString();
            historialPermiso.Permiso = new ML.Permiso();
            historialPermiso.Permiso.IdPermiso = dto.IdPermiso;
            historialPermiso.Permiso.FechaInicio = dto.FechaInicioPermiso;
            historialPermiso.Permiso.FechaFin = dto.FechaFinPermiso;
            historialPermiso.StatusPermiso = new ML.StatusPermiso();
            historialPermiso.StatusPermiso.Descripcion = dto.Status;
            historialPermiso.Autorizo = new ML.Empleado();
            historialPermiso.Autorizo.Nombre = dto.Nombre;
            historialPermiso.Autorizo.ApellidoPaterno = dto.ApellidoPaterno;
            historialPermiso.Autorizo.Correo = dto.Correo;
            historialPermiso.Autorizo.Departamento = new ML.Departamento();
            historialPermiso.Autorizo.Departamento.Descripcion = dto.Departamento;

            return historialPermiso; 
        }

    }
}
