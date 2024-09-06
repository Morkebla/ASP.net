namespace GameStore.Api.Dtos;

public record class GameDetailsDto(
    int id,
    string name,
    int genreId,
    decimal price,
    DateOnly releaseDate);
