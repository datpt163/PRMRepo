using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.ResponseMediator
{
    public class ResponseMediator
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public Object Data { get; set; }

        public ResponseMediator(bool isSucees, string message, Object data)
        {
            IsSuccess = isSucees;
            Message = message;
            Data = data;
        }
    }
}
