
namespace API.Options
{
    public class DatabaseConnectionOptions
    {
        public const string ConnectionStrings = "ConnectionStrings";
        public string DefaultConnection { get; set; } = string.Empty;
    }
}