using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace BodaInvitacionApp.Models
{
    public class InvitacionContext : DbContext
    {
        public InvitacionContext(DbContextOptions<InvitacionContext> options) : base(options) { }

        public DbSet<Confirmacion> Confirmacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var isPostgres = Database.ProviderName?.Contains("Npgsql") ?? false;

            if (isPostgres)
            {
                // Conversión para DateTime no-nullable
                var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                    v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

                // Conversión para DateTime nullable
                var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                    v => v.HasValue ? (v.Value.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)) : v,
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    foreach (var property in entityType.GetProperties())
                    {
                        if (property.ClrType == typeof(DateTime))
                        {
                            property.SetValueConverter(dateTimeConverter);
                        }
                        else if (property.ClrType == typeof(DateTime?))
                        {
                            property.SetValueConverter(nullableDateTimeConverter);
                        }
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
