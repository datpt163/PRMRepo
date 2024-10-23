using Capstone.Application.Common.FileService;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Labels.Command;
using MediatR;
using System.Text.Json;


namespace Capstone.Application.Module.Labels.CommandHandle
{
    public class DeleteDefaultLabelCommandHandle : IRequestHandler<DeleteDefaultLabelCommand, ResponseMediator>
    {
        private readonly IFileService _fileService;

        public DeleteDefaultLabelCommandHandle(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<ResponseMediator> Handle(DeleteDefaultLabelCommand request, CancellationToken cancellationToken)
        {
            string module = "Module";
            string project = "Projects";
            string folder = "Default";
            string fileName = "DefaultLabel.json";
            string path = Path.Combine(module, project, folder, fileName);
            var labels = JsonSerializer.Deserialize<List<Domain.Entities.Label>>(await _fileService.ReadFileAsync(path)) ?? new List<Domain.Entities.Label>();
            if (labels.Count() == 1)
                return new ResponseMediator("Cannot delete all default label ", null, 400);

            var label = labels.FirstOrDefault(x => x.Id == request.Id);
            if (label == null)
                return new ResponseMediator("Label not found", null, 404);

            labels.Remove(label);
            await _fileService.WriteFileAsync(path, JsonSerializer.Serialize(labels));
            return new ResponseMediator("", null);
        }
    }
}
