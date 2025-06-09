using ReservationService.Application.Common.Dtos;

namespace ReservationService.Application.UseCases.Reservations.SearchReservations;

public record SearchReservationsRequest
{
    public FiltersSearchReservations? Filters { get; set; }
    public PaginationDto Pagination { get; set; } = new();

    public record FiltersSearchReservations(
        int? UserId
    );
}
