using AutoMapper;

namespace ZooAPI.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<Pokemon, PokemonDTO>().ReverseMap();
            //cette ligne permet de dire qu'a l'aide du mapper on pourra passer de l'entité vers le DTO
            // et vice versa grace au .ReverseMap()
        }
    }
}
