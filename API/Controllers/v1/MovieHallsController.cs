using API.DTOs.MovieHallDTOs;
using API.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MovieHallsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MovieHallsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MovieHallCreateDto createDto)
        {
            try 
            {
                var movieHall = createDto.Adapt<MovieHall>();
                await _db.MovieHalls.AddAsync(movieHall);

                var isSuccess = await _db.SaveChangesAsync();
                if (isSuccess > 0)
                {
                    return Ok(movieHall);
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