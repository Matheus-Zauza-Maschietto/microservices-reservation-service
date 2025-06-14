using System.Net;
using System.Text;
using System.Text.Json;
using ReservationService.Infra.BooksService.Dtos;

namespace ReservationService.Infra.BooksService.Services;

public class ApiBooksService : IApiBooksService
{
    private readonly HttpClient _httpClient;

    public ApiBooksService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(ApiBooksService));
    }

    public async Task<BookDto> FindBookById(int bookId)
    {
        var response = await _httpClient.GetAsync($"/Book/{bookId}");
        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<BookDto>(responseBody)!;
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new Exception($"There isn't a book with id {bookId}.");
        }

        throw new Exception($"An error ocurred when trying find book: {responseBody}");
    }

    public async Task UpdateBookStatus(int bookId, TypeBookStatus typeStatus)
    {
        await FindBookById(bookId);

        var requestBody = JsonSerializer.Serialize(new UpdateBookStatusDto((int)typeStatus));
        var response = await _httpClient.PatchAsync(
            $"/BookInstance/{bookId}/Status",
            new StringContent(requestBody, Encoding.UTF8, "application/json")
        );

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            throw new Exception($"An error ocurred when updating book status to {typeStatus}: {responseBody}");
        }
    }
}
