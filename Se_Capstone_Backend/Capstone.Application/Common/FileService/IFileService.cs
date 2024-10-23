

namespace Capstone.Application.Common.FileService
{
    public interface IFileService
    {
        Task WriteFileAsync(string path, string content);
        Task<string> ReadFileAsync(string path);
    }
}
