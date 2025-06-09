using ReservationService.Application.Common.Dtos;
using ReservationService.Domain.Reservations;
using ReservationService.Domain.Reservations.Entities;
using ReservationService.Infra.BooksService.Dtos;
using ReservationService.Infra.BooksService.Services;

namespace ReservationService.Application.UseCases.Reservations.CreateReservation;

public class CreateReservationUseCase
{
    private readonly IApiBooksService _apiBooksService;
    private readonly IReservationsRepository _reservationsRepository;

    public CreateReservationUseCase(IApiBooksService apiBooksService, IReservationsRepository reservationsRepository)
    {
        _apiBooksService = apiBooksService;
        _reservationsRepository = reservationsRepository;
    }

    public async Task<EntityIdDto> ExecuteAsync(CreateReservationRequest request)
    {
        var book = await _apiBooksService.FindBookById(request.BookId);

        await _apiBooksService.UpdateBookStatus(book.Id, TypeBookStatus.Reserved);

        var reservation = new Reservation(request.UserId, book.Id);

        await _reservationsRepository.InsertAsync(reservation);
        await _reservationsRepository.CommitAsync();

        return new EntityIdDto(reservation.Id);
    }
}
