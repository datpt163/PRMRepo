using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.ResponseMediator
{
    public class ResponseMediator
    {
        public string? ErrorMessage { get; set; }

        public Object? Data { get; set; }

        public ResponseMediator(string errorMessage, Object? data)
        {
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}
