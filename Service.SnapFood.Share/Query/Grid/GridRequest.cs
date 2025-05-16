using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Query.Grid
{
    public class GridRequest
    {
#pragma warning disable IDE1006 

        public int take { get; set; } = 10;
        public int skip { get; set; } = 0;
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public Filter filter { get; set; }

        public List<Sort> sort { get; set; }
#pragma warning restore IDE1006
        public int ItemID { get; set; }
        public string? Field { get; set; }
        public bool FieldOption { get; set; }

        public GridRequest()
        {
            sort = new List<Sort>() { /*new Sort() { field = "Created", dir = "desc" }*/ };
            filter = new Filter();

        }

    }
}
