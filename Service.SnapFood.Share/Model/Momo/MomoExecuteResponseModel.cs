using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.Momo
{
    public class MomoExecuteResponseModel
    {
        public string OrderId { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;    
        public string OrderInfo { get; set; } = string.Empty;
        public int ResultCode { get; set; }
    }
}
