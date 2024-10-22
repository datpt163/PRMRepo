using Capstone.Application.Common.FileService;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Status.Command;
using Capstone.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Status.CommandHandle
{
    public class DeleteStatusDefaultCommandHandle : IRequestHandler<DeleteStatusDefaultCommand, ResponseMediator>
    {
        private readonly IFileService _fileService;

        public DeleteStatusDefaultCommandHandle(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<ResponseMediator> Handle(DeleteStatusDefaultCommand request, CancellationToken cancellationToken)
        {
            string module = "Module";
            string project = "Projects";
            string folder = "Default";
            string fileName = "DefaultStatus.json";
            string path = Path.Combine(module, project, folder, fileName);
            var statuses = JsonSerializer.Deserialize<List<Domain.Entities.Status>>(await _fileService.ReadFileAsync(path)) ?? new List<Domain.Entities.Status>();
            if (statuses.Count() == 1)
                return new ResponseMediator("Cannot delete all default status", null, 400);

            var status = statuses.FirstOrDefault(x => x.Id == request.Id);
            if (status == null)
                return new ResponseMediator("Status not found", null, 404);

            foreach (var stat in statuses)
            {
                if (stat.Position > status.Position)
                    stat.Position = stat.Position - 1;
            }
            statuses.Remove(status);
            await _fileService.WriteFileAsync(path, JsonSerializer.Serialize(statuses));
            return new ResponseMediator("", null);
        }
    }
}
