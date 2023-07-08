
namespace API.Entities
{
    public class MovieHall
    {
        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime StartDateTime { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}