using System;

namespace ReservationService.Application.Common.Dtos;

public record PaginationDto
{
    public int PageNumber { get; init; } = 1;
    public int QuantityRecords { get; init; } = 10;
}
