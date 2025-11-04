using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }
    }
}
