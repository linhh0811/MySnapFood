using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Manage.Query
{
    public class DiscountCodeQuery : BaseQuery
    {
        public bool IsActive { get; set; } = false;
    }
}
