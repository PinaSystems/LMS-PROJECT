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
            })
            .ForMember(dest => dest.RoleId, opt => 
            {
                opt.MapFrom(src => src.RoleId);
            })
            .ForMember(dest => dest.Role, opt => 
            {
                opt.MapFrom(src => src.Role.Name);
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
                opt.MapFrom(src => UrlHelper.baseUrl+"files/"+ src.FileName);
            });
            CreateMap<Assignment, AssignmentDto>()
            .ForMember(dest => dest.AttachmentFile, opt => 
            {
                opt.MapFrom(src => UrlHelper.baseUrl+"files/"+ src.AttachmentFile);
            });
            CreateMap<Nugget, NuggetDto>()
            .ForMember(dest => dest.LessonId, opt => 
            {
                opt.MapFrom(src => src.LessonId);
            })
            .ForMember(dest => dest.Lesson, opt => 
            {
                opt.MapFrom(src => src.Lesson.Name);
            })
            .ForMember(dest => dest.NuggetId, opt => 
            {
                opt.MapFrom(src => src.Id);
            })
            .ForMember(dest => dest.Nugget, opt => 
            {
                opt.MapFrom(src => src.Name);
            })
            .ForMember(dest => dest.ClassDate, opt => 
            {
                opt.MapFrom(src => src.ClassDate.ToString("dd/MM/yyyy"));
            });
        }

    }
}