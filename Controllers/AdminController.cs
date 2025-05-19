using Microsoft.AspNetCore.Mvc;

namespace Projet_salle_de_gym.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}