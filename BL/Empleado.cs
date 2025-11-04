using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class Empleado
    {
        private readonly DL.SistemaNominaContext _context;
        

        public Empleado(DL.SistemaNominaContext context)
        {
            _context = context;
         
        }

        public ML.Resultado GetAll()
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {

                var query = _context.GetAllEmpleadoDTO.FromSqlInterpolated($" EmpleadoGetAll").ToList();

                if (query.Count > 0)
                {
                    resultado.Objects = new List<object>();
                    foreach (DL.GetAllEmpleadoDTO obj in query)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.Departamento = new ML.Departamento();

                        empleado.IdEmpleado = obj.IdEmpleado;
                        empleado.Nombre = obj.Nombre;
                        empleado.ApellidoPaterno = obj.ApellidoPaterno;
                        empleado.ApellidoMaterno = obj.ApellidoMaterno;
                        empleado.FechaNacimiento = obj.FechaNacimiento;
                        empleado.RFC = obj.RFC;
                        empleado.NSS = obj.NSS;
                        empleado.CURP = obj.CURP;
                        empleado.Correo = obj.Correo;
                        empleado.FechaIngreso = obj.FechaIngreso;
                        empleado.SalarioBase = obj.SalarioBase;
                        empleado.NoFaltas = obj.NoFaltas;
                        empleado.Imagen = obj.Imagen;
                        empleado.Departamento.IdDepartamento = obj.IdDepartamento;
                        empleado.Departamento.Descripcion = obj.NombreDepartamento;

                        resultado.Objects.Add(empleado);
                        resultado.Correct = true;

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


        public static ML.Resultado GetDepartamentList()
        {
            ML.Resultado resultado = new ML.Resultado();
            try
            {
                using (DL.SistemaNominaContext context = new DL.SistemaNominaContext())
                {
                    var query = (from list in context.Departamentos

                                 select list).ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Descripcion = obj.Descripcion;
                            resultado.Objects.Add(departamento);
                            resultado.Correct = true;
                        }
                    }
                    else
                    {
                        resultado.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessagge = ex.Message;
            }
            return resultado;
        }

        public static ML.Resultado GetEmployeesList()
        {
            ML.Resultado resultado = new ML.Resultado();
            try
            {
                using (DL.SistemaNominaContext context = new DL.SistemaNominaContext())
                {
                    var query = (from list in context.Empleados

                                 select list).ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.Nombre = obj.Nombre;
                            empleado.CURP = obj.Curp;

                            resultado.Objects.Add(empleado);
                            resultado.Correct = true;
                        }
                    }
                    else
                    {
                        resultado.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessagge = ex.Message;
            }
            return resultado;
        }

        public ML.Resultado GetById(int IdEmpleado)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {

                var obj = (from emp in _context.Empleados
                           join dep in _context.Departamentos
                           on emp.IdDepartamento equals dep.IdDepartamento
                           where emp.IdEmpleado == IdEmpleado
                           select new
                           {
                               emp.IdEmpleado
                               ,
                               emp.Nombre
                               ,
                               emp.ApellidoPaterno
                               ,
                               emp.ApellidoMaterno
                               ,
                               emp.FechaNacimiento
                               ,
                               emp.Rfc
                               ,
                               emp.Nss
                               ,
                               emp.Curp
                               ,
                               emp.Correo
                               ,
                               emp.FechaIngreso
                               ,
                               emp.SalarioBase
                               ,
                               emp.NoFaltas
                               ,
                               emp.Imagen
                               ,
                               dep.IdDepartamento
                               ,
                               dep.Descripcion

                           }).SingleOrDefault();

                if (obj != null)
                {

                    ML.Empleado empleado = new ML.Empleado();
                    empleado.Departamento = new ML.Departamento();

                    empleado.IdEmpleado = obj.IdEmpleado;
                    empleado.Nombre = obj.Nombre;
                    empleado.ApellidoPaterno = obj.ApellidoPaterno;
                    empleado.ApellidoMaterno = obj.ApellidoMaterno;
                    empleado.FechaNacimiento = obj.FechaNacimiento.ToString();
                    empleado.RFC = obj.Rfc;
                    empleado.NSS = obj.Nss;
                    empleado.CURP = obj.Curp;
                    empleado.Correo = obj.Correo;
                    empleado.FechaIngreso = obj.FechaIngreso.ToString();
                    empleado.SalarioBase = obj.SalarioBase.Value;
                    empleado.NoFaltas = obj.NoFaltas.Value;
                    empleado.Imagen = obj.Imagen;
                    empleado.Departamento.IdDepartamento = obj.IdDepartamento;
                    empleado.Departamento.Descripcion = obj.Descripcion;
                    resultado.Object = empleado;
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

        public ML.Resultado Delete(int IdEmpleado)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {
                
                var obj = (from emp in _context.Empleados
                           where emp.IdEmpleado == IdEmpleado
                           select
                             emp
                           ).SingleOrDefault();

                if (obj != null)
                {
                    _context.Remove(obj);
                    int rowsAffected = _context.SaveChanges();

                    if (rowsAffected > 0)
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

        public ML.Resultado Add(ML.Empleado empleado)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Nombre", empleado.Nombre),
                    new SqlParameter("@ApellidoPaterno", empleado.ApellidoPaterno),
                    new SqlParameter("@ApellidoMaterno", empleado.ApellidoMaterno),
                    new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento.ToString()),
                    new SqlParameter("@RFC", empleado.RFC),
                    new SqlParameter("@NSS", empleado.NSS),
                    new SqlParameter("@CURP", empleado.CURP),
                    new SqlParameter("@Correo", empleado.Correo),
                    new SqlParameter("@FechaIngreso", empleado.FechaIngreso.ToString()),
                    new SqlParameter("@IdDepartamento", empleado.Departamento.IdDepartamento),
                    new SqlParameter("@SalarioBase", empleado.SalarioBase),
                    new SqlParameter("@NoFaltas", empleado.NoFaltas),
                    new SqlParameter("@Imagen", SqlDbType.VarBinary) { Value = (object)empleado.Imagen ?? DBNull.Value }
                };

                int query = _context.Database.ExecuteSqlRaw("EXEC EmpleadoAdd @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @RFC, @NSS, @CURP,@Correo ,@FechaIngreso, @IdDepartamento, @SalarioBase, @NoFaltas, @Imagen", parameters);


                //int query = _context.Database.ExecuteSqlInterpolated($" EmpleadoAdd {empleado.Nombre},{empleado.ApellidoPaterno},{empleado.ApellidoMaterno},{empleado.FechaNacimiento.ToString()},{empleado.RFC},{empleado.NSS},{empleado.CURP},{empleado.FechaIngreso.ToString()},{empleado.Departamento.IdDepartamento},{empleado.SalarioBase},{empleado.NoFaltas},{empleado.Imagen}");


                if (query == 1)
                {
                    resultado.Correct = true;
                    resultado.Correct = true;
                    int id = (from record in _context.Empleados orderby record.IdEmpleado descending select record.IdEmpleado).First();

                    if (id > 0)
                    {
                        resultado.Object = id;
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

        public ML.Resultado Update(ML.Empleado empleado)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {
                var query = (from emp in _context.Empleados
                             where emp.IdEmpleado == empleado.IdEmpleado
                             select emp).SingleOrDefault();


                if (query != null)
                {
                    query.Nombre = empleado.Nombre;
                    query.ApellidoPaterno = empleado.ApellidoPaterno;
                    query.ApellidoMaterno = empleado.ApellidoMaterno;
                    query.FechaNacimiento = DateOnly.Parse(empleado.FechaNacimiento);
                    query.Rfc = empleado.RFC;
                    query.Nss = empleado.NSS;
                    query.Curp = empleado.CURP;
                    query.Correo = empleado.Correo;
                    query.FechaIngreso = DateOnly.Parse(empleado.FechaIngreso);
                    query.IdDepartamento = empleado.Departamento.IdDepartamento;
                    query.SalarioBase = empleado.SalarioBase;
                    query.NoFaltas = empleado.NoFaltas;
                    query.Imagen = empleado.Imagen;

                    int rowsAffected = _context.SaveChanges();

                    if (rowsAffected > 0)
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
    }
}
