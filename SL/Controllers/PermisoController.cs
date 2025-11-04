using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController(BL.Permiso permiso) : ControllerBase
    {
        private readonly BL.Permiso _permiso = permiso;

        [Route("Add")]
        [HttpPost]
        public IActionResult PermisoAdd(ML.Permiso permiso)
        {
            ML.Resultado resultado = _permiso.Add(permiso);

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult PermisosGetAll()
        {
            ML.Resultado resultado = _permiso.GetAll();

            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("Historial/GetAll")]
        [HttpGet]
        public IActionResult HistorialPermisosGetAll()
        {
            ML.Resultado resultado = _permiso.HistorialGetAll();
            if (resultado.Correct)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [Route("Update")]
        [HttpPut]
        public IActionResult PermisoUpdate(int IdPermiso, int IdStatusPermiso, int IdEmpleado)
        {
            ML.Resultado resultado = _permiso.ProcesarSolicitud(IdPermiso,IdStatusPermiso,IdEmpleado);
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
