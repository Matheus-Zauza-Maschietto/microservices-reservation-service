using ReservationService.Domain.Reservations.Entities;

namespace ReservationService.Domain.Reservations;

public interface IReservationsRepository
{
    Task InsertAsync(Reservation reservation);
    Task<int> CommitAsync();
    Task<Reservation> GetById(int id);
    IQueryable<Reservation> GetQueryable();
    void Delete(Reservation reservation);
}
