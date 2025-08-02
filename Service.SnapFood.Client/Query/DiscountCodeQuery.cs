using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Client.Query
{
    public class DiscountCodeQuery : BaseQuery
    {
        public bool IsActive { get; set; } = false;
    }
}
