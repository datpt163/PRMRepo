using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.FileService
{
    public class FileService : IFileService
    {
        private string GetAbsolutePath(string relativePath)
        {
            string binPath = AppDomain.CurrentDomain.BaseDirectory;
            string currentDir = Directory.GetCurrentDirectory();
            string context = AppContext.BaseDirectory;
            string e = Environment.CurrentDirectory;
            Console.WriteLine("binPath: " + currentDir);
            Console.WriteLine("currentDir: " + binPath);
            Console.WriteLine("context: " + context);
            Console.WriteLine("Environment: " + e);
            return Path.Combine(currentDir, relativePath);
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
