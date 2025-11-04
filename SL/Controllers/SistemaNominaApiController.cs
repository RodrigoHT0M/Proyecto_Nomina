using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaNominaApiController(BL.Empleado empleado, BL.Usuario usuario) : ControllerBase
    {
        private readonly BL.Empleado _empleado = empleado;
        private readonly BL.Usuario _usuario = usuario;

        [Route("Empleado/GetAll")]
        [HttpGet]
        public IActionResult EmpleadoGetAll()
        {
            ML.Resultado resultado = _empleado.GetAll();

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }
        [Route("Empleado/GetById")]
        [HttpGet]
        public IActionResult EmpleadoGetById(int IdEmpleado)
        {
            ML.Resultado resultado = _empleado.GetById(IdEmpleado);

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("Empleado/Add")]
        [HttpPost]
        public IActionResult EmpleadoAdd(ML.Empleado empleado)
        {
            ML.Resultado resultado = _empleado.Add(empleado);

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }
        [Route("Empleado/Update")]
        [HttpPut]
        public IActionResult EmpleadoUpdate(ML.Empleado empleado)
        {
            ML.Resultado resultado = _empleado.Update(empleado);

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }
        [Route("Empleado/Delete")]
        [HttpDelete]
        public IActionResult EmpleadoDelete(int IdEmpleado)
        {
            ML.Resultado resultado = _empleado.Delete(IdEmpleado);

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("Usuario/GetAll")]
        [HttpGet]
        public IActionResult UsuarioGetAll()
        {
            ML.Resultado resultado = _usuario.GetAll();

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }


        [Route("Usuario/GetById")]
        [HttpGet]
        public IActionResult UsuarioGetById(int IdUsuario)
        {
            ML.Resultado resultado = _usuario.GetById(IdUsuario);

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("Usuario/Add")]
        [HttpPost]
        public IActionResult UsuarioAdd(ML.Usuario usuario)
        {
            ML.Resultado resultado = _usuario.Add(usuario);

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("Usuario/Update")]
        [HttpPut]
        public IActionResult UsuarioUpdate(ML.Usuario usuario)
        {
            ML.Resultado resultado = _usuario.Update(usuario);

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("Usuario/Delete")]
        [HttpDelete]
        public IActionResult UsuarioDelete(int IdUsuario)
        {
            ML.Resultado resultado = _usuario.Delete(IdUsuario);

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }



        [Route("ListaDepartamentos")]
        [HttpGet]
        public IActionResult GetListDeptos()
        {
            ML.Resultado resultado = BL.Empleado.GetDepartamentList();
            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("ListaRoles")]
        [HttpGet]
        public IActionResult GetListRols()
        {
            ML.Resultado resultado = BL.Usuario.GetRolList();
            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("ListaEmpleados")]
        [HttpGet]
        public IActionResult GetListEmployees()
        {
            ML.Resultado resultado = BL.Empleado.GetEmployeesList();
            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

    }
}
