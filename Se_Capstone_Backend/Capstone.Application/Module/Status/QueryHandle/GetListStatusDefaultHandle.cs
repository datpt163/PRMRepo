using Capstone.Application.Common.FileService;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Status.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Capstone.Application.Module.Status.QueryHandle
{
    public class GetListStatusDefaultHandle : IRequestHandler<GetListStatusDefaultQuery, ResponseMediator>
    {
        private readonly IFileService _fileService;
        public GetListStatusDefaultHandle(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<ResponseMediator> Handle(GetListStatusDefaultQuery request, CancellationToken cancellationToken)
        {
            string module = "Module";
            string project = "Projects";
            string folder = "Default";
            string fileName = "DefaultStatus.json";
            string path = Path.Combine(module, project, folder, fileName);
            var statuses = (JsonSerializer.Deserialize<List<Domain.Entities.Status>>(await _fileService.ReadFileAsync(path)))?? new List<Domain.Entities.Status>();
            
            return new ResponseMediator("", statuses.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Position = x.Position,
                Color = x.Color,
            }).OrderBy(c => c.Position).ToList());
        }
    }
}
