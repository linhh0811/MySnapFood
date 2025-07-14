namespace Service.SnapFood.Client.Dto.Bill
{
    public class ComboItemsArchiveDto
    {
        public Guid Id { get; set; }
        public Guid BillDetailsId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
