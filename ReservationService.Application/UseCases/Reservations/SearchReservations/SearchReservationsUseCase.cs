using Microsoft.EntityFrameworkCore;
using ReservationService.Application.Common.Dtos;
using ReservationService.Application.Dtos.Reservations;
using ReservationService.Domain.Reservations;

namespace ReservationService.Application.UseCases.Reservations.SearchReservations;

public class SearchReservationsUseCase
{
    private readonly IReservationsRepository _reservationsRepository;

    public SearchReservationsUseCase(IReservationsRepository reservationsRepository)
    {
        _reservationsRepository = reservationsRepository;
    }

    public async Task<PaginatedResultDto<ReservationDto>> ExecuteAsync(SearchReservationsRequest request)
    {
        var query = _reservationsRepository.GetQueryable();

        if (request.Filters is not null)
        {
            if (request.Filters.UserId != null)
            {
                query = query.Where(r => r.UserId == request.Filters.UserId);
            }
        }

        var totalRecords = await query.CountAsync();

        var result = query
            .OrderBy(r => r.Id)
            .Skip(request.Pagination.QuantityRecords * (request.Pagination.PageNumber - 1))
            .Take(request.Pagination.QuantityRecords)
            .Select(r => new ReservationDto
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                Status = r.Status.Select(s => new ReservationStatusDto
                {
                    Id = s.Id,
                    Type = new EnumDto
                    {
                        Code = (int)s.Type,
                        Description = s.Type.ToString()
                    },
                    Date = s.Date
                })
            }
        );

        return new PaginatedResultDto<ReservationDto>(
            await result.ToListAsync(),
            request.Pagination.PageNumber,
            request.Pagination.QuantityRecords,
            totalRecords
        );
    }
}
