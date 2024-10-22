using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.TokenService
{
    public interface ITokenRevocationService
    {
        public Task<(bool isSuccess, string errorMessage)> RevocationTokenAsync(Guid userId); 
    }
}
