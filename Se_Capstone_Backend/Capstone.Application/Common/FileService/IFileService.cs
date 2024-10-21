using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.FileService
{
    public interface IFileService
    {
        Task WriteFileAsync(string path, string content);
        Task<string> ReadFileAsync(string path);
    }
}
