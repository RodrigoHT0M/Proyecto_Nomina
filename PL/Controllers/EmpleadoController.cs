using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {

        private readonly HttpClient _httpClient;
        public EmpleadoController(HttpClient httpClient)
        {

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7261");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {



            ML.Empleado empleado = new ML.Empleado();
            empleado.Departamento = new ML.Departamento();
            empleado.Empleados = new List<object>();

            var response = _httpClient.GetAsync("/api/SistemaNominaApi/Empleado/GetAll").Result;

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
                            ML.Empleado empleadoobj = JsonConvert.DeserializeObject<ML.Empleado>(json);

                            if (empleadoobj != null)
                            {
                                empleado.Empleados.Add(empleadoobj);
                            }
                        }
                    }
                }
            }
            return View(empleado);

        }


        [HttpPost]
        public IActionResult Form(ML.Empleado empleado, IFormFile imgEmpleadoInput)
        {
            ML.Resultado listDep = BL.Empleado.GetDepartamentList();
            if (listDep.Correct)
            {
                empleado.Departamento.Departamentos = listDep.Objects;
            }
            empleado.Empleados = new List<object>();
            if (imgEmpleadoInput != null && imgEmpleadoInput.Length > 0)

            {
                using (var memoryStream = new MemoryStream())

                {
                    imgEmpleadoInput.CopyToAsync(memoryStream);
                    empleado.Imagen = memoryStream.ToArray();
                }
            }
            if (empleado.IdEmpleado > 0)
            {
                var postTask = _httpClient.PutAsJsonAsync<ML.Empleado>("/api/SistemaNominaApi/Empleado/Update", empleado); //Serializar 

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
                        return View(empleado);
                    }

                }
                else
                {
                    return View(empleado);
                }
            }
            else
            {
                var postTask = _httpClient.PostAsJsonAsync<ML.Empleado>("/api/SistemaNominaApi/Empleado/Add", empleado); //Serializar 

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
                        return View(empleado);
                    }

                }
                else
                {
                    return View(empleado);
                }
            }

        }


        [HttpGet]
        public IActionResult Form(int? IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Departamento = new ML.Departamento();



            if (IdEmpleado > 0)
            {
                var postTask = _httpClient.GetAsync("/api/SistemaNominaApi/Empleado/GetById?IdEmpleado=" + IdEmpleado);

                postTask.Wait();

                var result = postTask.Result;


                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Resultado>();

                    readTask.Wait();

                    var resultEmpleado = readTask.Result;

                    if (resultEmpleado.Correct)
                    {
                        empleado = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(readTask.Result.Object.ToString());
                    }
                }
            }
        
            ML.Resultado listDep = BL.Empleado.GetDepartamentList();
            if (listDep.Correct)
            {
                empleado.Departamento.Departamentos = listDep.Objects;
            }

            return View(empleado);
        }

        [HttpGet]
        public IActionResult Delete(int? IdEmpleado)
        {
            var postTask = _httpClient.DeleteAsync("/api/SistemaNominaApi/Empleado/Delete?IdEmpleado=" + IdEmpleado);

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
