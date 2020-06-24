using System.Linq;
using AutoMapper;
using datingapp.Dtos;
using datingapp.Models;

    /*
        "We need to tell Automapper about the mappings we need to support."
     */

namespace datingapp.Helpers
{
    public class AutoMapperProfiles : Profile // Automapper uses profiles to understand src and dest of what it's mapping
    {
        public AutoMapperProfiles() // mapping configs
        {
            ////////////////////////////////////////////////////////////////////////////////////
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                // '.ForMember' allows custom configs for individual members of a class
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {   // .ResolveUsing() was deprecated and removed from AutoMapper v8+
                    // http://docs.automapper.org/en/stable/8.0-Upgrade-Guide.html#resolveusing
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });

            ////////////////////////////////////////////////////////////////////////////////////
            CreateMap<User, UserForDetailedDto>().ForMember(dest => dest.PhotoUrl, opt =>
            {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt =>
            {   // .ResolveUsing() was deprecated and removed from AutoMapper v8+
                // http://docs.automapper.org/en/stable/8.0-Upgrade-Guide.html#resolveusing
                opt.MapFrom(d => d.DateOfBirth.CalculateAge());
            });

            ///////////////////////////////////////////////////////////////////////////////////
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
        }
        

    }
}
