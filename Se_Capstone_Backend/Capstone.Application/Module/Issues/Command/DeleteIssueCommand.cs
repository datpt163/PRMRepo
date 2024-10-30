using Capstone.Application.Common.ResponseMediator;
using MediatR;

namespace Capstone.Application.Module.Issues.Command
{
    public class DeleteIssueCommand : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }    
    }
}
