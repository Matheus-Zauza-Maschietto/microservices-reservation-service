using ReservationService.Infra.BooksService.Dtos;

namespace ReservationService.Infra.BooksService.Services;

public interface IApiBooksService
{
    Task<BookDto> FindBookById(int bookId);
    Task UpdateBookStatus(int bookId, TypeBookStatus typeStatus);
}
