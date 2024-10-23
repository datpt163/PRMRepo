
namespace Capstone.Application.Common.FileService
{
    public class FileService : IFileService
    {
        private string GetAbsolutePath(string relativePath)
        {
            string binPath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(binPath, relativePath);
        }

        public async Task WriteFileAsync(string relativePath, string content)
        {

            var absolutePath = GetAbsolutePath(relativePath);
            await File.WriteAllTextAsync(absolutePath, content);
        }

        public async Task<string> ReadFileAsync(string relativePath)
        {
            try
            {
                var absolutePath = GetAbsolutePath(relativePath);
                    Console.WriteLine(absolutePath);
                return await File.ReadAllTextAsync(absolutePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }
    }
}
