using AutoMapper;
using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Comments.Command;
using Capstone.Application.Module.Comments.CommentDTOs;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Comments.CommandHandle
{
    public class AddCommentCommandHandle : IRequestHandler<AddCommentCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public AddCommentCommandHandle(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            _mapper = mapper;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }   

        public async Task<ResponseMediator> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.VerifyTokenAsync(request.Token);
            if(user == null)
                return new ResponseMediator("User not found", null);

            if (_unitOfWork.Users.FindOne(x => x.Id == user.Id) == null)
                return new ResponseMediator("User not found", null);

            if (_unitOfWork.Issues.FindOne(x => x.Id == request.IssueId) == null)
                return new ResponseMediator("Issue not found", null);

            if (string.IsNullOrEmpty(request.Content))
                return new ResponseMediator("Content empty", null);

            var comment = new Comment() { Content = request.Content, UserId = user.Id, IssueId = request.IssueId, CreatedAt = DateTime.Now };
            _unitOfWork.Comments.Add(comment);
            await _unitOfWork.SaveChangesAsync();
            var response = _mapper.Map<CommentDTO>(comment);
            return new ResponseMediator("", response);

        }
    }
}
