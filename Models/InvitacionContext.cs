using Microsoft.EntityFrameworkCore;

namespace BodaInvitacionApp.Models
{
    public class InvitacionContext : DbContext
    {
        public InvitacionContext(DbContextOptions<InvitacionContext> options) : base(options) { }

        public DbSet<Confirmacion> Confirmacion { get; set; }
    }
}
