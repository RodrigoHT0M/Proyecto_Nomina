using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PL.Controllers
{
    public class PermisoController : Controller
    {
        private readonly HttpClient _httpClient;
        public PermisoController(HttpClient httpClient)
        {

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7261");
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult GetAll()
        {
            

            ML.Permiso permiso = new ML.Permiso();
            permiso.Empleado = new ML.Empleado();
            permiso.Empleado.Departamento = new ML.Departamento();
            permiso.Permisos = new List<object>();



            var response = _httpClient.GetAsync("/api/Permiso/GetAll").Result;

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
                            ML.Permiso permisoobj = JsonConvert.DeserializeObject<ML.Permiso>(json);

                            if (permisoobj != null)
                            {
                                permiso.Permisos.Add(permisoobj);
                            }
                        }
                    }
                }
            }

            return View(permiso);
        }

        [HttpGet]
        public IActionResult Form(int? IdPermiso)
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Form(ML.Permiso permiso)
        {
            permiso.StatusPermiso = new ML.StatusPermiso();
            permiso.StatusPermiso.IdStatusPermiso = 1;
            permiso.Empleado = new ML.Empleado();
            permiso.Empleado.IdEmpleado = 8;
            DateTime myDate = DateTime.Now;
            permiso.FechaSolicitud = myDate.ToString("yyyy-MM-dd");

            var postTask = _httpClient.PostAsJsonAsync<ML.Permiso>("/api/Permiso/Add", permiso); //Serializar 

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
                    return View(permiso);
                }

            }
            else
            {
                return View(permiso);
            }
            return View();
        }

        public IActionResult HistorialGetAll()
        {
            ML.HistorialPermiso historialPermiso = new ML.HistorialPermiso();
            
            historialPermiso.HistorialPermisos = new List<object>();

            var respone = _httpClient.GetAsync("/api/Permiso/Historial/GetAll").Result;

            if (respone.IsSuccessStatusCode)
            {
                ML.Resultado getallresult = respone.Content.ReadFromJsonAsync<ML.Resultado>().Result;
                if (getallresult.Correct)
                {
                    foreach(var obj in getallresult.Objects)
                    {
                        string data = obj.ToString();

                        ML.HistorialPermiso hp = JsonConvert.DeserializeObject<ML.HistorialPermiso>(data);

                        historialPermiso.HistorialPermisos.Add(hp);
                        
                    }
                }
            }
            return View(historialPermiso);
        }



    }
}
