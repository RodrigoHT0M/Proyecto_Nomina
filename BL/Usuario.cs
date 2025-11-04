using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        private readonly DL.SistemaNominaContext _context;

        public Usuario(DL.SistemaNominaContext context)
        {
            _context = context;
        }

        public ML.Resultado GetAll()
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {

                var query = _context.GetAllUsuarioDTO.FromSqlInterpolated($" UsuarioGetAll").ToList();

                if (query.Count > 0)
                {
                    resultado.Objects = new List<object>();

                    foreach (DL.GetAllUsuarioDTO obj in query)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Empleado = new ML.Empleado();
                        usuario.Rol = new ML.Rol();
                        usuario.Empleado.Departamento = new ML.Departamento();

                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.Empleado.Nombre = obj.Nombre;
                        usuario.Empleado.ApellidoPaterno = obj.ApellidoPaterno;
                        usuario.Nombre = obj.NombreUsuario;
                        usuario.PasswordHash = obj.PasswordHash;
                        usuario.Status = obj.Status;
                        usuario.Rol.IdRol = obj.IdRol;
                        usuario.Rol.Descripcion = obj.Rol;
                        usuario.Empleado.Departamento.Descripcion = obj.Departamento;
                        resultado.Objects.Add(usuario);
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

        public static ML.Resultado GetRolList()
        {
            ML.Resultado resultado = new ML.Resultado();
            try
            {
                using (DL.SistemaNominaContext context = new DL.SistemaNominaContext())
                {
                    var query = (from list in context.Rols

                                 select list).ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = obj.IdRol;
                            rol.Descripcion = obj.Descripcion;
                            resultado.Objects.Add(rol);
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

        public ML.Resultado GetById(int IdUsuario)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {

                var obj = (from user in _context.Usuarios
                           join rol in _context.Rols
                           on user.IdRol equals rol.IdRol
                           join emp in _context.Empleados
                           on user.IdEmpleado equals emp.IdEmpleado
                           where user.IdUsuario == IdUsuario
                           select new
                           {
                               user.IdUsuario,
                               user.NombreUsuario,
                               user.IdEmpleado,
                               user.PasswordHash,
                               user.Status,
                               rol.IdRol,
                               rol.Descripcion,
                               emp.Curp 

                           }).SingleOrDefault();

                if (obj != null)
                {
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Empleado = new ML.Empleado();
                    usuario.Rol = new ML.Rol();

                    usuario.IdUsuario = obj.IdUsuario;
                    usuario.Empleado.IdEmpleado = obj.IdEmpleado.Value;
                    usuario.Empleado.CURP = obj.Curp;
                    usuario.Nombre = obj.NombreUsuario;
                    usuario.PasswordHash = obj.PasswordHash;
                    usuario.Status = obj.Status.Value;
                    usuario.Rol.IdRol = obj.IdRol;
                    usuario.Rol.Descripcion = obj.Descripcion;
                    resultado.Object = usuario;
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

        public ML.Resultado Delete(int IdUsuario)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {

                var obj = (from user in _context.Usuarios
                           where user.IdUsuario == IdUsuario
                           select
                             user
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

        public ML.Resultado Add(ML.Usuario usuario)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {
                int query = _context.Database.ExecuteSqlInterpolated($" UsuarioAdd {usuario.Empleado.IdEmpleado},{usuario.Nombre},{usuario.PasswordHash},{usuario.Status},{usuario.Rol.IdRol}");


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

        public ML.Resultado Update(ML.Usuario usuario)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {
                var query = (from user in _context.Usuarios
                             where user.IdUsuario == usuario.IdUsuario
                             select user).SingleOrDefault();


                if (query != null)
                {
                    query.NombreUsuario = usuario.Nombre;
                    query.IdEmpleado = usuario.Empleado.IdEmpleado;
                    query.PasswordHash = usuario.PasswordHash;
                    query.Status = usuario.Status;
                    query.IdRol = usuario.Rol.IdRol;


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
