using AutoMapper;
using Capstone.Application.Module.Auths.Response;
using Capstone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Application.Module.Projects.Response;
namespace Capstone.Application.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleDTO>();
            CreateMap<Project, ProjectDTO>()
            .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => (src.Lead != null) ? src.Lead.Id : (Guid?)null))
            .ForMember(dest => dest.LeadName, opt => opt.MapFrom(src => (src.Lead != null) ? src.Lead.UserName : null));
            CreateMap<Project, ProjectDetailResponse>()
            .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => (src.Lead != null ) ? src.Lead.Id : (Guid?)null))
            .ForMember(dest => dest.LeadName, opt => opt.MapFrom(src => (src.Lead != null ) ? src.Lead.UserName : null))
            .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Users.ToList()));

             CreateMap<User, UserForProjectDetailDTO>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<Permission, PermissionDTO>();
        }
    }
}
