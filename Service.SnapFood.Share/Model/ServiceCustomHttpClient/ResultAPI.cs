using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.ServiceCustomHttpClient
{
    public class ResultAPI
    {
        public ResultAPI(StatusCode status)
        {
            Status = status;
        }
        public StatusCode Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
