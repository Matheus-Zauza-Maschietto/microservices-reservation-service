namespace ReservationService.Infra.BooksService.Dtos;

public record class UpdateBookStatusDto
{
    public int Status { get; init; }

    public UpdateBookStatusDto(int status)
    {
        Status = status;
    }
}
