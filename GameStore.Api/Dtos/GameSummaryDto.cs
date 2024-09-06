namespace GameStore.Api.Dtos;

public record class GameSummaryDto(
    int id,
    string name,
    string genre,
    decimal price,
    DateOnly releaseDate);
