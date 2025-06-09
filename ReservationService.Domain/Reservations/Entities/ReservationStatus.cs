namespace ReservationService.Domain.Reservations.Entities;

public class ReservationStatus
{
    public int Id { get; private set; }
    public TypeReservationStatus Type { get; private set; }
    public DateTimeOffset Date { get; private set; }

    public ReservationStatus(TypeReservationStatus type, DateTimeOffset date)
    {
        Type = type;
        Date = date;
    }
}
