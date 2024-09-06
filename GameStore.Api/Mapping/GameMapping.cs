using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping
{
    public static class GameMapping
    {
        public static Game ToEntity(this CreateGameDto game, int id)
        {
            return new Game()
            {
                name = game.name,
                genreId = game.genreId,
                price = game.price,
                releaseDate = game.releaseDate
            };
        }

        public static Game ToEntity(this UpdateGameDto game, int id)
        {
            return new Game()
            {
                id = id,
                name = game.name,
                genreId = game.genreId,
                price = game.price,
                releaseDate = game.releaseDate
            };
        }

        public static GameSummaryDto ToGameSummaryDto(this Game game)
        {
            string genreName = game.genre?.name ?? "Unknown Genre";

            return new GameSummaryDto
            (
                game.id,
                game.name,
                genreName,
                game.price,
                game.releaseDate
            );
        }

        public static GameDetailsDto ToGameDetailsDto(this Game game)
        {
            string genreName = game.genre?.name ?? "Unknown Genre";

            return new GameDetailsDto
            (
                game.id,
                game.name,
                game.genreId,
                game.price,
                game.releaseDate
            );
        }
    }
}
