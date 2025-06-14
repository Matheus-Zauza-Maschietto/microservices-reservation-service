using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationService.Api.Extensions;
using ReservationService.Application.UseCases.Reservations.CreateReservation;
using ReservationService.Application.UseCases.Reservations.DeleteReservation;
using ReservationService.Application.UseCases.Reservations.SearchReservations;
using ReservationService.Domain.Reservations;
using ReservationService.Infra.BooksService.Services;
using ReservationService.Infra.Data.PostgresSql.Context;
using ReservationService.Infra.Data.PostgresSql.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IApiBooksService, ApiBooksService>();
builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();
builder.Services.AddSwaggerGen();

builder.Services
    .AddHttpClient<ApiBooksService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBooksService:BaseAddress")!);
    });

builder.Services.AddOpenApi();

var app = builder.Build();
app.MapSwagger();


app.MigrateDatabase<ApplicationDbContext>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/reservations", async ([FromBody] CreateReservationRequest request, [FromServices] CreateReservationUseCase useCase) =>
{
    var entityId = await useCase.ExecuteAsync(request);

    return Results.Ok(entityId);
});

app.MapGet("/reservations/user/{userId}", async ([FromRoute] int userId, [FromServices] SearchReservationsUseCase useCase) =>
{
    var request = new SearchReservationsRequest
    {
        Filters = new SearchReservationsRequest.FiltersSearchReservations(userId)
    };
    var paginatedResults = await useCase.ExecuteAsync(request);

    return Results.Ok(paginatedResults);
});

app.MapDelete("/reservations/{reservationId}", async ([FromRoute] int reservationId, [FromServices] DeleteReservationUseCase useCase) =>
{
    var request = new DeleteReservationRequest
    {
        ReservationId = reservationId
    };

    await useCase.ExecuteAsync(request);

    return Results.NoContent();
});

app.Run();