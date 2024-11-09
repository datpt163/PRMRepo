using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Comments.Command;
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
    public class DeleteCommentCommandHandle : IRequestHandler<DeleteCommentCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        public DeleteCommentCommandHandle(IUnitOfWork unitOfWork, IJwtService jwtService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<ResponseMediator> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _unitOfWork.Comments.FindOne(x => x.Id == request.Id);
            if (comment == null)
                return new ResponseMediator("Comment not found", null, 404);
            var user = await _jwtService.VerifyTokenAsync(request.Token);
            if (user == null)
                return new ResponseMediator("User not found", null, 404);
            var roles = await _userManager.GetRolesAsync(user);
            var role = _unitOfWork.Roles.Find(x => x.Name != null && x.Name == (roles.FirstOrDefault() == null ? "" : roles.FirstOrDefault())).Include(c => c.Permissions).FirstOrDefault();

            if (user.Id != comment.UserId && !(role != null && role.Name != null && role.Permissions.Select(x => x.Name).Contains("DELETE_ALL_COMMENT")))
                return new ResponseMediator("Do not have permission to delete this comment", null);

            _unitOfWork.Comments.Remove(comment);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", null);
        }
    }
}
