using Service.SnapFood.Manage.Enums;
using Service.SnapFood.Share.Model.Enum;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using Service.SnapFood.Share.Query.Grid;

namespace Service.SnapFood.Manage.Query
{
    public class BillQuery:BaseQuery
    {
      
       public StatusOrder Status {  get; set; }
        public bool IsBanHang { get; set; } = false;
    }
}
