using System.ComponentModel.DataAnnotations;
using GameStore.Api.Entities;

namespace GameStore.Api.Dtos
{
    public record class CreateGameDto(
        [Required][StringLength(50)] string name,
        int genreId,
        [Range(1, 100)] decimal price,
        DateOnly releaseDate
    )
    {
        internal Game ToEntity()
        {
            return new Game
            {
                name = this.name,
                genreId = this.genreId,
                price = this.price,
                releaseDate = this.releaseDate
            };
        }
    }
}
