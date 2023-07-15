using System.Reflection;
using API.DTOs.MovieDTOs;
using API.DTOs.MovieHallDTOs;
using API.Entities;
using Mapster;

namespace API.Mapping
{
    public static class MapsterConfiguration
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<Movie, MovieDto>.NewConfig();
            TypeAdapterConfig<MovieCreateDto, Movie>.NewConfig();
            TypeAdapterConfig<MovieUpdateDto, Movie>.NewConfig();
            TypeAdapterConfig<MovieHallCreateDto, MovieHall>.NewConfig();
            TypeAdapterConfig<MovieHallUpdateDto, MovieHall>.NewConfig();
            TypeAdapterConfig<MovieHall, MovieHallReadOnlyDto>.NewConfig();
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}