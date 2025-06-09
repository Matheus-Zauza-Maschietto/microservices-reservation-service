namespace ReservationService.Application.UseCases.Reservations.CreateReservation;

public class CreateReservationRequest
{
    public int UserId { get; init; }
    public int BookId { get; init; }
}
