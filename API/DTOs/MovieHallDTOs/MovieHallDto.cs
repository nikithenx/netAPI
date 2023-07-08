
namespace API.DTOs.MovieHallDTOs
{
    public class MovieHallDto
    {
        
    }

    public class MovieHallCreateDto
    {
        public int NumberOfSeats { get; set; }
        public DateTime StartDateTime { get; set; }

        public int MovieId { get; set; }
    }

    public class MovieHallUpdateDto
    {
        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime StartDateTime { get; set; }

        public int MovieId { get; set; }
    }
}