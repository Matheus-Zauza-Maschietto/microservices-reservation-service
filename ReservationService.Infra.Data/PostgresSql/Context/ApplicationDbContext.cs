using Microsoft.EntityFrameworkCore;
using ReservationService.Domain.Reservations.Entities;

namespace ReservationService.Infra.Data.PostgresSql.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected ApplicationDbContext()
    {
    }
}
