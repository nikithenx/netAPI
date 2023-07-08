using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey("Id");
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Title).HasColumnName("Title");
            builder.Property(x => x.Genre).HasColumnName("Genre");
            builder.Property(x => x.DurationInMinutes).HasColumnName("DurationInMinutes");
            builder.Property(x => x.Revenue).HasColumnName("Revenue");

            builder.HasMany(x => x.Halls)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();
        }
    }
}