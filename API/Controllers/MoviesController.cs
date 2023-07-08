using API.DTOs.MovieDTOs;
using API.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try 
            {
                var movies = await _db.Movies.ProjectToType<MovieDto>().ToListAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(GetAll)}: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MovieCreateDto createDto)
        {
            try 
            {
                var movie = createDto.Adapt<Movie>();
                await _db.Movies.AddAsync(movie);

                var isSuccess = await _db.SaveChangesAsync();
                if (isSuccess > 0)
                {
                    return Ok(movie);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(Create)}: {ex.Message}");
                return BadRequest();
            }
        }
    }
}