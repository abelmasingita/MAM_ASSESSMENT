public class TaxCalculator
{
    private const decimal BasicTaxRate = 0.10m;
    private const decimal ImportDutyRate = 0.05m;

    public static decimal CalculateTax(Product product)
    {
        decimal tax = 0;

        if (!product.IsExempt)
        {
            tax += RoundUpToNearest0Point05(product.Price * BasicTaxRate);
        }

        if (product.IsImported)
        {
            tax += RoundUpToNearest0Point05(product.Price * ImportDutyRate);
        }

        return tax;
    }

    private static decimal RoundUpToNearest0Point05(decimal value)
    {
        return Math.Ceiling(value * 100) / 100;
    }

    public static Receipt GenerateReceipt(List<Product> products)
    {
        Receipt receipt = new Receipt();

        foreach (var product in products)
        {
            decimal tax = CalculateTax(product);
            decimal totalPriceWithTax = product.Price + tax;

            receipt.Products.Add(new Product
            {
                Name = product.Name,
                Price = totalPriceWithTax,
                IsImported = product.IsImported,
                IsExempt = product.IsExempt
            });

            receipt.TotalSalesTax += tax;
            receipt.TotalPrice += totalPriceWithTax;
        }

        return receipt;
    }
}
