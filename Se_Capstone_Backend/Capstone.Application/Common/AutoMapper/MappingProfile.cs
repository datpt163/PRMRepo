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
            CreateMap<Project, CreateProjectResponse>();
            CreateMap<Permission, PermissionDTO>();
        }
    }
}
