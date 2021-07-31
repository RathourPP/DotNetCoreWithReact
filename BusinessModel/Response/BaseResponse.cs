using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyProjectApi.BusinessModel.Response
{
    public class BaseResponse
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
