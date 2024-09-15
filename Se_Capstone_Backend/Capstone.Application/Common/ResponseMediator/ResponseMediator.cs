using Capstone.Application.Common.Paging;
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
        public int StatusCode { get; set; } = 0;

        public ResponseMediator(string errorMessage, Object? data)
        {
            ErrorMessage = errorMessage;
            Data = data;
        }

        public ResponseMediator(string errorMessage, Object? data, int statusCode)
        {
            ErrorMessage = errorMessage;
            Data = data;
            StatusCode = statusCode;
        }
    }
}
