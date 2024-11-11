using AutoMapper;
using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Comments.Command;
using Capstone.Application.Module.Comments.CommentDTOs;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Comments.CommandHandle
{
    public class UpdateCommentCommandHandle : IRequestHandler<UpdateCommentCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UpdateCommentCommandHandle(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseMediator> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _unitOfWork.Comments.FindOne(x => x.Id == request.Id);
            if (comment == null)
                return new ResponseMediator("Comment not found", null, 404);
            var user = await _jwtService.VerifyTokenAsync(request.Token);
            if (user == null)
                return new ResponseMediator("User not found", null, 404);

            var roles = await _userManager.GetRolesAsync(user);
            var role = _unitOfWork.Roles.Find(x => x.Name != null && x.Name == (roles.FirstOrDefault() == null ? "" : roles.FirstOrDefault())).Include(c => c.Permissions).FirstOrDefault();


            if (user.Id != comment.UserId && !(role != null && role.Name != null && role.Permissions.Select(x => x.Name).Contains("UPDATE_ALL_COMMENT")) )
                return new ResponseMediator("Do not have permission to update this comment", null);

            if (string.IsNullOrEmpty(request.Content))
                return new ResponseMediator("Content empty", null);
            comment.Content = request.Content;
            comment.UpdatedAt = DateTime.Now;
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.SaveChangesAsync();
            var response = _mapper.Map<CommentDTO>(comment);
            return new ResponseMediator("", response);
        }
    }
}
