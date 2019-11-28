namespace HerbShop.Models
{
    public class Product
    {
        public long Id { get; set; }
        public long HerbId { get; set; }
        public decimal Price { get; set; }
        public long Unit { get; set; }
        public long Quantity { get; set; }
        public string UnitSymbol { get; set; }
    }
}
