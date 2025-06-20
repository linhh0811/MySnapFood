namespace Service.SnapFood.Manage.Dto.BillDetails
{
    public class BillDetailsDto
    {

        public Guid Id { get; set; }
        public string ItemsName { get; set; }
            public string ImageUrl { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
             public decimal PriceEndow { get; set; }


    }
}
