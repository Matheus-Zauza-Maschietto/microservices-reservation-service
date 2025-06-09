namespace ReservationService.Application.Common.Dtos;

public class PaginatedResultDto<TData>
{
    public IEnumerable<TData> Data { get; set; } = [];
    public int PageNumber { get; set; }
    public int QuantityRecords { get; set; }
    public int TotalRecords { get; set; }

    public PaginatedResultDto(IEnumerable<TData> data, int pageNumber, int quantityRecords, int totalRecords)
    {
        Data = data;
        PageNumber = pageNumber;
        QuantityRecords = quantityRecords;
        TotalRecords = totalRecords;
    }
}
