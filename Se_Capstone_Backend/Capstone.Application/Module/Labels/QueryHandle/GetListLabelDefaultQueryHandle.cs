using Capstone.Application.Common.FileService;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Labels.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Labels.QueryHandle
{
    public class GetListLabelDefaultQueryHandle : IRequestHandler<GetListLabelDefaultQuery, ResponseMediator>
    {
        private readonly IFileService _fileService;
        public GetListLabelDefaultQueryHandle(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<ResponseMediator> Handle(GetListLabelDefaultQuery request, CancellationToken cancellationToken)
        {
            string module = "Module";
            string project = "Projects";
            string folder = "Default";
            string fileName = "DefaultLabel.json";
            string path = Path.Combine(module, project, folder, fileName);
            var labels = (JsonSerializer.Deserialize<List<Domain.Entities.Label>>(await _fileService.ReadFileAsync(path))) ?? new List<Domain.Entities.Label>();

            return new ResponseMediator("", labels.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToList());
        }
    }
}
