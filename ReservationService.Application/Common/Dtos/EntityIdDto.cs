namespace ReservationService.Application.Common.Dtos;

public class EntityIdDto
{
    public int Id { get; init; }

    public EntityIdDto(int id)
    {
        Id = id;
    }
}
