using Microsoft.EntityFrameworkCore;

namespace MinAPI
{
    public static class ConfigureEndpoints
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            var movies = app.MapGroup("/movies");

            movies.MapGet("/", GetAllMovies);
            movies.MapGet("/{id}", GetMovie);
            movies.MapPost("/", CreateMovie);
            movies.MapPut("/{id}", UpdateMovie);
            movies.MapDelete("/{id}", DeleteMovie);

            return app;
        }

        private static async Task<IResult> GetAllMovies(ApplicationDbContext db)
            => TypedResults.Ok(await db.Movies.ToArrayAsync());

        private static async Task<IResult> GetMovie(int id, ApplicationDbContext db)
            => await db.Movies.FindAsync(id)
                is Movie movie
                ? TypedResults.Ok(movie)
                : TypedResults.NotFound();     

        private static async Task<IResult> CreateMovie(Movie movie, ApplicationDbContext db)
        {
            if (movie is null) return TypedResults.BadRequest();

            await db.Movies.AddAsync(movie);
            var isSuccess = await db.SaveChangesAsync() > 0;

            return isSuccess
                ? TypedResults.Created($"/movies/{movie.Id}", movie)
                : TypedResults.BadRequest();
        }

        private static async Task<IResult> UpdateMovie(int id, Movie updateMovie, ApplicationDbContext db)
        {
            var movie = await db.Movies.FindAsync(id);

            if (movie is null) return TypedResults.NotFound();

            movie.Title = updateMovie.Title;
            movie.Genre = updateMovie.Genre;
            movie.DurationInMinutes = updateMovie.DurationInMinutes;
            movie.Revenue = updateMovie.Revenue;

            var isSuccess = await db.SaveChangesAsync() > 0;

            return isSuccess
                ? TypedResults.NoContent()
                : TypedResults.BadRequest();
        }

        private static async Task<IResult> DeleteMovie(int id, ApplicationDbContext db)
        {
            if (await db.Movies.FindAsync(id) is Movie movie)
            {
                db.Movies.Remove(movie);
                await db.SaveChangesAsync();
                return TypedResults.Ok(movie.Id);
            }

            return TypedResults.NotFound();
        }
    }
}