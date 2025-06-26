using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_APP.Models.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class FailedResponseModel
    {
        public string ErrorCode { get; set; }
        public string ErrorDetail { get; set; }
    }
}
