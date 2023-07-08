
using API.DTOs.MovieHallDTOs;

namespace API.DTOs.MovieDTOs
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal Revenue { get; set; }
        public ICollection<MovieHallReadOnlyDto> Halls { get; set; }
    }

    public class MovieCreateDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal Revenue { get; set; }
    }

    public class MovieUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal Revenue { get; set; }
    }
}