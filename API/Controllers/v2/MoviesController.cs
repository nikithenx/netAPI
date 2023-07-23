using API.DTOs.MovieDTOs;
using API.Entities;
using API.RateLimiting;
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
        [LimitRequests(MaxRequests = 1, Window = 1)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var query = "SELECT * FROM Movies";
            using var connection = _db.CreateConnection();
            var movies = await connection.QueryAsync<Movie>(query);
            return Ok(movies);
        }

        [MapToApiVersion("2.0")]
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MovieCreateDto createDto)
        {
            var query = "Insert into Movies (Title, Genre, DurationInMinutes, Revenue) Values (@Title, @Genre, @DurationInMinutes, @Revenue)";
            using var connection = _db.CreateConnection();
            _ = await connection.ExecuteAsync(query, createDto);
            return Created(nameof(Create), createDto);
        }
    }
}