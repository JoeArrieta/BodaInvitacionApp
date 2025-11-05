using BodaInvitacionApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BodaInvitacionApp.Controllers
{
    public class InvitacionController : Controller
    {
        private readonly InvitacionContext _context;

        public InvitacionController(InvitacionContext context)
        {
            _context = context;
        }

        //Vista para registrar invitado
        public IActionResult CrearInvitado()
        {
            return View();
        }

        //Guarda invitado
        [HttpPost]
        public IActionResult CrearInvitado(Confirmacion confirmacion) {
            if (ModelState.IsValid)
            {
                confirmacion.PasesDisponibles = confirmacion.NumeroDePersonas;

                _context.Confirmacion.Add(confirmacion);
                _context.SaveChanges();
                return RedirectToAction("Invitados");
            }
            return View(confirmacion);
        }
        [HttpPost]
        public IActionResult EliminarInvitado(int id)
        {
            var invitado = _context.Confirmacion.FirstOrDefault(x => x.Id == id);
            if (invitado is null)
                return NotFound();

            _context.Confirmacion.Remove(invitado);
            _context.SaveChanges();

            return RedirectToAction("Invitados");
        }

        //Listado de invitados
        public IActionResult Invitados()
        {
            var invitados = _context.Confirmacion.OrderBy(x => x.Id).ToList();
            return View(invitados);
        }

        public IActionResult ConfirmarAsistencia(int id)
        {
            var invitado = _context.Confirmacion.FirstOrDefault(i => i.Id == id);
            if (invitado == null)
                return NotFound();

            return View(invitado);
        }

        [HttpPost]
        public IActionResult ConfirmarAsistencia(Confirmacion confirmacion)
        {
            if (ModelState.IsValid)
            {
                var invitado = _context.Confirmacion.FirstOrDefault(i => i.Id == confirmacion.Id);
                if (invitado is null)
                    return NotFound();
                
                if(confirmacion.NumeroDePersonas > invitado.NumeroDePersonas)
                {
                    ModelState.AddModelError("", $"Solo puede confirmar hasta {invitado.NumeroDePersonas} personas.");
                    return View(confirmacion);
                }
                
                invitado.ConfirmoAsistencia = true;
                invitado.ConfirmoNoPases = confirmacion.NumeroDePersonas;
                invitado.Mensaje = confirmacion.Mensaje;
                invitado.FechaRegistro = DateTime.Now;

                _context.Update(invitado);
                _context.SaveChanges();

                if (invitado.ConfirmoNoPases == 0)
                    return RedirectToAction("Agradecimiento");
                else
                    return RedirectToAction("Gracias");
            }
            return View(confirmacion);
        }

        public IActionResult EnviarWhatsApp(int id)
        {
            var invitado = _context.Confirmacion.FirstOrDefault(i => i.Id == id);
            if (invitado is null)
                return NotFound();

            string mensaje = $"Hola {invitado.Nombre}, estás invitado a nuestra boda 'Josue y Diana'. " +
                $"Será un honor contar con tu presencia, " +
                $"por favor confirma aquí: https://bodadianayjosue.onrender.com/?id={invitado.Id}";
            string url = $"https://wa.me/52{invitado.Telefono}?text={Uri.EscapeDataString(mensaje)}";

            invitado.EnvioInvitacion = true;
            _context.Update(invitado);
            _context.SaveChanges();

            return Redirect(url);
        }

        // Vista de agradecimiento
        public IActionResult Gracias()
        {
            return View();
        }
    }
}
