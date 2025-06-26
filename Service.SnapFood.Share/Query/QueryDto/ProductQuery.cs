using Service.SnapFood.Share.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Query.QueryDto
{
    public class ProductQuery : BaseQuery
    {
        public Guid CategoryId { get; set; }
    }
}
