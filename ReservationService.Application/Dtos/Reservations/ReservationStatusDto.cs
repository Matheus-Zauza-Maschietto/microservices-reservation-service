using ReservationService.Application.Common.Dtos;

namespace ReservationService.Application.Dtos.Reservations;

public class ReservationStatusDto
{
    public int Id { get; set; }
    public EnumDto Type { get; set; } = new();
    public DateTimeOffset Date { get; set; }
}
