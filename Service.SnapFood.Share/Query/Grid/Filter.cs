using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Query.Grid
{
    public class Filter
    {
        //Nếu không có logic thì hiểu mặc định là so sánh bằng
        public string? Value { get; set; }
        public string? Field { get; set; }
        public string? Method { get; set; }


        public List<Filter> Filters { get; set; } = new List<Filter>();
        public string? Logic { get; set; }
    }
}
