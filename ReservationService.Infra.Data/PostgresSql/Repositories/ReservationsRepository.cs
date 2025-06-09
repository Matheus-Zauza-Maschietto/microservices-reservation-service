using Microsoft.EntityFrameworkCore;
using ReservationService.Domain.Reservations;
using ReservationService.Domain.Reservations.Entities;
using ReservationService.Infra.Data.PostgresSql.Context;

namespace ReservationService.Infra.Data.PostgresSql.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InsertAsync(Reservation reservation)
    {
        await _dbContext
            .Reservations
            .AddAsync(reservation);
    }

    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Reservation> GetById(int id)
    {
        return await _dbContext
            .Reservations
            .SingleOrDefaultAsync(r => r.Id == id) ?? throw new Exception($"There isn't a reservation with id: {id}");
    }

    public IQueryable<Reservation> GetQueryable()
    {
        return _dbContext
            .Reservations
            .AsQueryable();
    }

    public void Delete(Reservation reservation)
    {
        _dbContext
            .Reservations
            .Remove(reservation);
    }
}
