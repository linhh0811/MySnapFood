using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.ServiceCustomHttpClient
{
    public class ResponseErrorAPI
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public Error? Errors { get; set; }
        public string? TraceId { get; set; }

        public bool? Success { get; set; }
        public string? Message { get; set; }
        public string? BuildQuery { get; set; }
        public object? Data { get; set; }
    }
}
