﻿using AutoMapper;
using Capstone.Application.Module.Auths.Response;
using Capstone.Domain.Entities;
using Capstone.Application.Module.Projects.Response;
using Capstone.Application.Module.Comments.CommentDTOs;
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
            .ForMember(dest => dest.LeadAvatar, opt => opt.MapFrom(src => (src.Lead != null) ? src.Lead.Avatar : null))
             .ForMember(dest => dest.LeadPosition, opt => opt.MapFrom(src => (src.Lead != null) ? (src.Lead.Position != null ? src.Lead.Position.Title : null) : null))
            .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Users.ToList()));

             CreateMap<User, UserForProjectDetailDTO>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
             .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => ( src.Position != null ? src.Position.Title : null)));

            CreateMap<Permission, PermissionDTO>();
            CreateMap<Issue, Application.Module.Issues.DTO.IssueDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<Issue, Application.Module.Issues.DTO.IssueDTO2>();
        }
    }
}
