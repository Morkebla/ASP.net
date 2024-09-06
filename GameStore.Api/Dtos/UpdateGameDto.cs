namespace GameStore.Api.Dtos;
using System.ComponentModel.DataAnnotations;

public record class UpdateGameDto(
    [Required][StringLength(50)]string name,
    int genreId,
    [Range(1,100)]decimal price,
    DateOnly releaseDate
);