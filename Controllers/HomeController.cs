using System.Diagnostics;
using BodaInvitacionApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BodaInvitacionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InvitacionContext _context;

        public HomeController(ILogger<HomeController> logger, InvitacionContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int id)
        {
            bool esAdmin = false;
            var misPases = 0;
            var nombreInvitado = "";
            var admin = _context.Confirmacion.FirstOrDefault(w => w.Id == id && w.Telefono == "5587260861" || w.Id == id && w.Telefono == "4951077806");
            var invitado = _context.Confirmacion.FirstOrDefault(i => i.Id == id);
            if (admin is not null)
            {
                esAdmin = true;
            }
            if (invitado is not null)
            {
                misPases = invitado.NumeroDePersonas;
                nombreInvitado = invitado.Nombre;
                id = invitado.Id;
            }

            ViewBag.esAdmin = esAdmin;
            ViewBag.id = id;
            ViewBag.misPases = misPases;
            ViewBag.invitado = nombreInvitado;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
