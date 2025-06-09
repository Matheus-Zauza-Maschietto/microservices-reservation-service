using ReservationService.Domain.Reservations;

namespace ReservationService.Application.UseCases.Reservations.DeleteReservation;

public class DeleteReservationUseCase
{
    private readonly IReservationsRepository _reservationsRepository;

    public DeleteReservationUseCase(IReservationsRepository reservationsRepository)
    {
        _reservationsRepository = reservationsRepository;
    }

    public async Task ExecuteAsync(DeleteReservationRequest request)
    {
        var reservation = await _reservationsRepository.GetById(request.ReservationId);

        _reservationsRepository.Delete(reservation);

        await _reservationsRepository.CommitAsync();
    }
}
