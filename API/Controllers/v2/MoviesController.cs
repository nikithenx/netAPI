using API.DTOs.MovieDTOs;
using API.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class MoviesController : ControllerBase
    {
        private readonly DapperContext _db;

        public MoviesController(DapperContext db)
        {
            _db = db;
        }

        [MapToApiVersion("2.0")]
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try 
            {
                var query = "SELECT * FROM Movies";
                using var connection = _db.CreateConnection();
                var movies = await connection.QueryAsync<Movie>(query);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(GetAll)}: {ex.Message}");
                return BadRequest();
            }
        }

        [MapToApiVersion("2.0")]
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MovieCreateDto createDto)
        {
            try 
            {
                var query = "Insert into Movies (Title, Genre, DurationInMinutes, Revenue) Values (@Title, @Genre, @DurationInMinutes, @Revenue)";
                using var connection = _db.CreateConnection();
                _ = await connection.ExecuteAsync(query, createDto);
                return Created(nameof(Create), createDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(Create)}: {ex.Message}");
                return BadRequest();
            }
        }
    }
}