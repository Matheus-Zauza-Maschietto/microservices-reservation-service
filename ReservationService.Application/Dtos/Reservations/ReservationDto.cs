namespace ReservationService.Application.Dtos.Reservations;

public class ReservationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public IEnumerable<ReservationStatusDto> Status { get; set; } = [];
}
