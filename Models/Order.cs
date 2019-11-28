namespace HerbShop.Models
{
    public class Order
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public long Quantity { get; set; }
        public decimal Total { get; set; }
        public string Description { get; set; }
        public string DateAndTime { get; set; }
    }
}
