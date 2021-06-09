using System.Linq;
using AutoMapper;
using OAWA.Data.Dtos;
using OAWA.Data.Enums;
using OAWA.Data.Models;

namespace OAWA.Data.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.Name, opt => 
            {
                opt.MapFrom(src => src.Name+ (string.IsNullOrEmpty(src.LastName)?"":" "+src.LastName));
            });
            CreateMap<User, UserForDetailDto>();            
            CreateMap<UserForUpdateDto, User>().ForAllMembers(action => action.Condition((src, dest, srcMember) => srcMember!=null));
            CreateMap<UserForRegisterDto, User>();
            
            CreateMap<User, UserForViewDto>();
            
            CreateMap<User, UserForReducedViewDto>();
            CreateMap<Course, CourseForViewDto>();
            CreateMap<NewsLetter, NewsletterForViewDto>()
            .ForMember(dest => dest.FileType, opt => 
            {
                opt.MapFrom(src => System.IO.Path.GetExtension(src.FileName));
            })
            .ForMember(dest => dest.FileName, opt => 
            {
                opt.MapFrom(src => UrlHelper.baseUrl+"newsletters/"+ src.FileName);
            });
        }

    }
}