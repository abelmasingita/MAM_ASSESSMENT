public class Receipt
{
    public List<Product> Products { get; set; } = new List<Product>();
    public decimal TotalSalesTax { get; set; }
    public decimal TotalPrice { get; set; }
}
