using Capstone.Application.Common.FileService;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Status.Command;
using MediatR;
using System.Text.Json;

namespace Capstone.Application.Module.Status.CommandHandle
{
    public class UpdateStatusDefaultCommandHandle : IRequestHandler<UpdateStatusDefaultCommand, ResponseMediator>
    {
        private readonly IFileService _fileService;

        public UpdateStatusDefaultCommandHandle(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<ResponseMediator> Handle(UpdateStatusDefaultCommand request, CancellationToken cancellationToken)
        {
            string module = "Module";
            string project = "Projects";
            string folder = "Default";
            string fileName = "DefaultStatus.json";
            string path = Path.Combine(module, project, folder, fileName);
            var statuses = JsonSerializer.Deserialize<List<Domain.Entities.Status>>(await _fileService.ReadFileAsync(path)) ?? new List<Domain.Entities.Status>();
            var status = statuses.FirstOrDefault(x => x.Id == request.Id);
            if (status == null)
                return new ResponseMediator("Status not found", null, 404);
            if (string.IsNullOrEmpty(request.Name))
                return new ResponseMediator("Name empty", null, 400);

            if (string.IsNullOrEmpty(request.Color))
                return new ResponseMediator("Color empty", null, 400);

            var statusCheckDuplicateTitle = statuses.FirstOrDefault(x => x.Id != request.Id && x.Name.Trim().ToUpper() == request.Name.Trim().ToUpper());
            if (statusCheckDuplicateTitle != null)
                return new ResponseMediator("This name status is availble", null, 400);

            status.Name = request.Name;
            status.Description = request.Description;
            status.Color = request.Color;
            status.IsDone = request.IsDone;
            await _fileService.WriteFileAsync(path, JsonSerializer.Serialize(statuses));
            return new ResponseMediator("", status);
        }
    }
}
