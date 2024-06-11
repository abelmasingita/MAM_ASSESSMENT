
class Program
{
    static void Main(string[] args)
    {
        List<Product> products = new List<Product>();

        Console.WriteLine("Enter items (type 'done' to finish):");
        while (true)
        {
            Console.Write("Enter item (format: 'quantity name at price'): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "done")
                break;

            var product = ParseInput(input);
            if (product != null)
            {
                products.Add(product);
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        var receipt = TaxCalculator.GenerateReceipt(products);
        PrintReceipt(receipt);
    }

    static Product ParseInput(string input)
    {
        try
        {
            var parts = input.Split(new[] { " at " }, StringSplitOptions.None);
            if (parts.Length != 2)
                return null;

            var pricePart = parts[1];
            var detailsPart = parts[0];

            decimal price = decimal.Parse(pricePart);
            bool isImported = detailsPart.Contains("imported");
            bool isExempt = IsExempt(detailsPart);

            var name = detailsPart.Replace("imported", "").Trim();

            return new Product
            {
                Name = name,
                Price = price,
                IsImported = isImported,
                IsExempt = isExempt
            };
        }
        catch
        {
            return null;
        }
    }

    static bool IsExempt(string details)
    {
        string[] exemptItems = { "book", "chocolate", "pill", "food" };
        foreach (var item in exemptItems)
        {
            if (details.Contains(item))
                return true;
        }
        return false;
    }

    static void PrintReceipt(Receipt receipt)
    {
        foreach (var product in receipt.Products)
        {
            Console.WriteLine($"{(product.IsImported ? "imported " : "")}{product.Name}: {product.Price:F2}");
        }

        Console.WriteLine($"Sales Taxes: {receipt.TotalSalesTax:F2}");
        Console.WriteLine($"Total: {receipt.TotalPrice:F2}");
        Console.WriteLine();
    }
}