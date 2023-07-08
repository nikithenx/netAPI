using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Configurations
{
    public class MovieHallConfiguration : IEntityTypeConfiguration<MovieHall>
    {
        public void Configure(EntityTypeBuilder<MovieHall> builder)
        {
            builder.ToTable("MovieHalls");
            builder.HasKey("Id");
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.NumberOfSeats).HasColumnName("NumberOfSeats");
            builder.Property(x => x.StartDateTime).HasColumnName("StartDateTime");
            builder.Property(x => x.MovieId).HasColumnName("MovieId");

            builder.HasOne(x => x.Movie)
                .WithMany(x => x.Halls)
                .HasForeignKey(x => x.MovieId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();                
        }
    }
}