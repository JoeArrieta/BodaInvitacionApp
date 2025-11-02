using System;
using System.ComponentModel.DataAnnotations;

namespace BodaInvitacionApp.Models
{
    public class Confirmacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El numero de pases es obligatorio")]
        public int NumeroDePersonas { get; set; }
        [Required(ErrorMessage = "El numero de mesa es obligatorio")]
        public int NomeroDeMesa { get; set; }
        public string? Mensaje { get; set; }
        public DateTime FechaRegistro { get; set; }

        [Required(ErrorMessage = "El numero de teléfono es obligatorio")]
        public string Telefono { get; set; } = null!;
        public bool EnvioInvitacion { get; set; }
        public bool ConfirmoAsistencia { get; set; }
        public int ConfirmoNoPases {  get; set; }
        public int PasesDisponibles { get; set; }
        public string? LinkWhatsApp { get; set; }

    }
}
