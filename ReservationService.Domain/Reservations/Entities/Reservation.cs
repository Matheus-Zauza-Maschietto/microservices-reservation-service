namespace ReservationService.Domain.Reservations.Entities;

public class Reservation
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int BookId { get; private set; }
    public List<ReservationStatus> Status { get; private set; } = [];

    protected Reservation() { }

    public Reservation(int userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
        Status.Add(new ReservationStatus(TypeReservationStatus.Active, DateTimeOffset.UtcNow));
    }

    public void FinishReservation()
    {
        Status.Add(new ReservationStatus(TypeReservationStatus.Finished, DateTimeOffset.UtcNow));
    }
}
