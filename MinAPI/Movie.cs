using System.ComponentModel.DataAnnotations.Schema;

namespace MinAPI
{
    [Table("Movies")]
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal Revenue { get; set; }
    }
}