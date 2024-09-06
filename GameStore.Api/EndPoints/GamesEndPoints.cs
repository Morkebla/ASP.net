using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Api.EndPoints;

public static class GamesEndPoints
{
    const string getGameEndPointName = "GetGame";

    private static readonly List<GameSummaryDto> games =
    [
        new(
            1,
            "Street Fighter II",
            "Fighting",
            19.99m,
            new DateOnly(1992, 7, 15)),
        new(
            2,
            "Final Fantasy XIV",
            "RPG",
            59.99m,
            new DateOnly(2010, 9, 30)),
        new(
            3,
            "FIFA 23",
            "Sports",
            69.99m,
            new DateOnly(2022, 9, 27)),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.games
        .Include(game => game.genre)
        .Select(game => game.ToGameSummaryDto())
        .AsNoTracking()
        .ToListAsync());

        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
            .WithName(getGameEndPointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(getGameEndPointName, new { id = game.id }, game.ToGameDetailsDto());
        })
        .WithParameterValidation();

        // PUT /games/1
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame,GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });


        //DELETE /games/1
        group.MapDelete("/{id}", async(int id, GameStoreContext dbContext) =>
        {
            await dbContext.games.Where(game => game.id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });
        return group;
    }
}
