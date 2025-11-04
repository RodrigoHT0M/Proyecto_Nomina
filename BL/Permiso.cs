using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Permiso
    {
        private readonly DL.SistemaNominaContext _context;
        private readonly ListGetAllHistorialPermisoDTO _listGetAllHistorialPermisoDTO;

        public Permiso(DL.SistemaNominaContext context, ListGetAllHistorialPermisoDTO listGetAllHistorialPermisoDTO)
        {
            _context = context;
            _listGetAllHistorialPermisoDTO = listGetAllHistorialPermisoDTO;
        }

        public ML.Resultado GetAll()
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {

                var query = _context.GetAllPermisoDTO.FromSqlInterpolated($" PermisoGetAll").ToList();

                if (query.Count > 0)
                {
                    resultado.Objects = new List<object>();

                    foreach (DL.GetAllPermisoDTO obj in query)
                    {
                        ML.Permiso permiso = new ML.Permiso();
                        permiso.Empleado = new ML.Empleado();
                        //permiso.Autorizo = new ML.Empleado();
                        permiso.Empleado.Departamento = new ML.Departamento();
                        permiso.StatusPermiso = new ML.StatusPermiso();

                        permiso.IdPermiso = obj.IdPermiso;
                        permiso.Empleado.Nombre = obj.Nombre;
                        permiso.Empleado.ApellidoPaterno = obj.ApellidoPaterno; permiso.Empleado.Departamento.Descripcion = obj.Departamento;
                        permiso.FechaSolicitud = obj.FechaSolicitud;
                        permiso.FechaInicio = obj.FechaInicio;
                        permiso.FechaFin = obj.FechaFin;
                        permiso.HoraInicio = obj.HoraInicio.ToString().Substring(0, 5);
                        permiso.HoraFin = obj.HoraFin.ToString().Substring(0, 5);
                        permiso.Motivo = obj.Motivo;
                        permiso.StatusPermiso.Descripcion = obj.Estatus;
                        //permiso.Autorizo.IdEmpleado = obj.Autorizo.Value;
                        //permiso.Autorizo.Nombre = obj.EmpleadoAutorizo;
                        //permiso.Autorizo.ApellidoPaterno = obj.ApellidoPaternoAutorizo;
                        //permiso.Autorizo.Correo = obj.CorreoAutorizo;

                        resultado.Correct = true;
                        resultado.Objects.Add(permiso);
                    }





                }
                else
                {
                    resultado.Correct = false;
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessagge = ex.Message;
            }
            return resultado;
        }

        public ML.Resultado Add(ML.Permiso permiso)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {
                int query = _context.Database.ExecuteSqlInterpolated($" PermisoAdd {permiso.Empleado.IdEmpleado},{permiso.FechaSolicitud.ToString()},{permiso.FechaInicio.ToString()},{permiso.FechaFin.ToString()},{permiso.HoraInicio},{permiso.HoraFin},{permiso.Motivo},{permiso.StatusPermiso.IdStatusPermiso}");


                if (query == 1)
                {
                    resultado.Correct = true;

                }
                else
                {
                    resultado.Correct = false;
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessagge = ex.Message;

            }
            return resultado;
        }


        public ML.Resultado ProcesarSolicitud(int IdPermiso, int IdStatusPermiso, int IdEmpleado)
        {
            ML.Resultado resultado = new ML.Resultado();
            try
            {
                var query = (from permiso in _context.Permisos

                             where permiso.IdPermiso == IdPermiso

                             select permiso).SingleOrDefault();

                if (query != null)
                {
                    query.IdStatusPermiso = IdStatusPermiso;
                    query.Autorizo = IdEmpleado;

                    int rowsAffected = _context.SaveChanges();

                    if (rowsAffected != 0)
                    {
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                    }

                }
                else
                {
                    resultado.Correct = false;
                }
            }


            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessagge = ex.Message;
            }
            return resultado;
        }

        public ML.Resultado HistorialGetAll()
        {
            ML.Resultado resultado = new ML.Resultado();
            try
            {
                List<GetAllHistorialPermisoDTO> obj = _listGetAllHistorialPermisoDTO.GetAll();

                if (obj.Count > 0)
                {
                    resultado.Objects = new List<object>();
                    foreach (var dto in obj)
                    {
                        ML.HistorialPermiso historialPermiso = ML.HistorialPermisoMapper.Map(dto);
                        resultado.Objects.Add(historialPermiso);


                    }

                    resultado.Correct = resultado.Objects.Count > 0 ? true : false;
                }
                else
                {
                    resultado.Correct = false;
                }


            }
            catch (Exception ex)
            {
                resultado.ErrorMessagge = ex.Message;
                resultado.Correct = false;
            }
            return resultado;
        }

    }
}
