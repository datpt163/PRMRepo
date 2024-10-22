using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Module.Auth.TokenBlackList
{
    public interface ITokenBlacklistService
    {
        Task<bool> BlacklistTokenAsync(string token, int authorizeCode);
        Task<int?> IsTokenBlacklistedAsync(string token);
    }

}
