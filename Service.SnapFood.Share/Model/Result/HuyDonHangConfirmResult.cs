using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.Result
{
    public class HuyDonHangConfirmResult
    {
        public bool Success { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
