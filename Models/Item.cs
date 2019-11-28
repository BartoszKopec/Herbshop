namespace HerbShop.Models
{
    public class Item
    {
        public long Id { get; set; }
        public long HerbId { get; set; }
        public decimal Price { get; set; }
        public long Unit { get; set; }
        public long Quantity { get; set; }
        public string UnitSymbol { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int QuantityInCart { get; set; }
        public decimal TotalAmount => QuantityInCart * Price;
    }
}
