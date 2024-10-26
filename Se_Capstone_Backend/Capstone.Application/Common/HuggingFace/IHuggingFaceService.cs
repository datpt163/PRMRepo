using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.HuggingFace
{
    public interface IHuggingFaceService
    {
        Task<string> GetResponseAsync(string prompt, string systemMessage = "You are a helpful assistant.", int maxTokens = 4096);
    }
}

