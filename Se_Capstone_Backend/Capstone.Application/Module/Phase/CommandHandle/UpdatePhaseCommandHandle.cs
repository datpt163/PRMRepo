using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Phase.Command;
using Capstone.Infrastructure.Repository;
using MediatR;


namespace Capstone.Application.Module.Phase.CommandHandle
{
    public class UpdatePhaseCommandHandle : IRequestHandler<UpdatePhaseCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePhaseCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(UpdatePhaseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
