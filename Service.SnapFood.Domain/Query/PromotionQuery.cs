using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Query
{
    public class PromotionQuery:BaseQuery
    {
        public PromotionType PromotionType { get; set; }

    }
}
