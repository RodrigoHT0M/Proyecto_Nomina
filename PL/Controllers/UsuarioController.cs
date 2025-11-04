using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;
        public UsuarioController(HttpClient httpClient)
        {

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7261");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {


            ML.Usuario usuario = new ML.Usuario();
            usuario.Empleado = new ML.Empleado();
            usuario.Rol = new ML.Rol();
            usuario.Usuarios = new List<object>();

            var response = _httpClient.GetAsync("/api/SistemaNominaApi/Usuario/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                ML.Resultado getAllResult = response.Content.ReadFromJsonAsync<ML.Resultado>().Result;

                if (getAllResult != null)
                {
                    if (getAllResult.Correct)
                    {
                        foreach (var obj in getAllResult.Objects)
                        {
                            string json = obj.ToString();
                            ML.Usuario usuarioobj = JsonConvert.DeserializeObject<ML.Usuario>(json);

                            if (usuarioobj != null)
                            {
                                usuario.Usuarios.Add(usuarioobj);
                            }
                        }
                    }
                }
            }

            return View(usuario);
        }


        [HttpGet]
        public IActionResult Form(int IdUsuario)
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Empleado = new ML.Empleado();
            usuario.Rol = new ML.Rol();

            if (IdUsuario > 0)
            {
                var postTask = _httpClient.GetAsync("/api/SistemaNominaApi/Usuario/GetById?IdUsuario=" + IdUsuario);

                postTask.Wait();

                var result = postTask.Result;


                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Resultado>();

                    readTask.Wait();

                    var resultEmpleado = readTask.Result;

                    if (resultEmpleado.Correct)
                    {
                        usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                    }
                }
            }
            ML.Resultado listRol = BL.Usuario.GetRolList();
            if (listRol.Correct)
            {
                usuario.Rol.Roles = listRol.Objects;
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Form(ML.Usuario usuario)
        {
            ML.Resultado listRol = BL.Usuario.GetRolList();
            if (listRol.Correct)
            {
                usuario.Rol.Roles = listRol.Objects;
            }
            if (usuario.IdUsuario > 0)
            {
                var postTask = _httpClient.PutAsJsonAsync<ML.Usuario>("/api/SistemaNominaApi/Usuario/Update", usuario); //Serializar 

                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)

                {
                    var readTask = result.Content.ReadAsAsync<ML.Resultado>();

                    readTask.Wait();

                    var resultPersona = readTask.Result;

                    if (resultPersona.Correct)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        return View(usuario);
                    }

                }
                else
                {
                    return View(usuario);
                }
            }
            else
            {
                var postTask = _httpClient.PostAsJsonAsync<ML.Usuario>("/api/SistemaNominaApi/Usuario/Add", usuario); //Serializar 

                postTask.Wait();



                var result = postTask.Result;

                if (result.IsSuccessStatusCode)

                {
                    var readTask = result.Content.ReadAsAsync<ML.Resultado>();

                    readTask.Wait();

                    var resultPersona = readTask.Result;

                    if (resultPersona.Correct)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        return View(usuario);
                    }

                }
                else
                {
                    return View(usuario);
                }
            }
        }

        [HttpGet]
        public IActionResult Delete(int IdUsuario)
        {

            var postTask = _httpClient.DeleteAsync("/api/SistemaNominaApi/Usuario/Delete?IdUsuario=" + IdUsuario);

            postTask.Wait();

            var result = postTask.Result;


            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ML.Resultado>();

                readTask.Wait();

                var resultEmpleado = readTask.Result;

                if (resultEmpleado.Correct)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    return RedirectToAction("GetAll");

                }

            }
            else
            {
                return RedirectToAction("GetAll");
            }
        }
    }
}
