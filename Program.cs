//E-commerce Platform Search Function
using System;
using System.Linq;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }

    public Product(int id, string name, string category)
    {
        ProductId = id;
        ProductName = name;
        Category = category;
    }

    public override string ToString()
    {
        return $"{ProductId} - {ProductName} ({Category})";
    }
}

public class SearchUtility
{
    public static Product LinearSearch(Product[] products, string name)
    {
        foreach (var product in products)
        {
            if (product.ProductName.Equals(name, StringComparison.OrdinalIgnoreCase))
                return product;
        }
        return null;
    }

    public static Product BinarySearch(Product[] products, string name)
    {
        int left = 0;
        int right = products.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int comparison = string.Compare(products[mid].ProductName, name, StringComparison.OrdinalIgnoreCase);

            if (comparison == 0)
                return products[mid];
            else if (comparison < 0)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return null;
    }
}

class Program
{
    static void Main()
    {
        Product[] products = new Product[]
        {
            new Product(1, "Laptop", "Electronics"),
            new Product(2, "T-shirt", "Apparel"),
            new Product(3, "Keyboard", "Electronics"),
            new Product(4, "Shoes", "Footwear"),
            new Product(5, "Mouse", "Electronics")
        };

        // 🔍 Linear Search
        Console.WriteLine("🔍 Linear Search:");
        var result1 = SearchUtility.LinearSearch(products, "Shoes");
        Console.WriteLine(result1 != null ? $"Found: {result1}" : "Product not found");

        // 🔍 Binary Search (on sorted array)
        var sortedProducts = products.OrderBy(p => p.ProductName).ToArray();
        Console.WriteLine("\n🔍 Binary Search:");
        var result2 = SearchUtility.BinarySearch(sortedProducts, "Shoes");
        Console.WriteLine(result2 != null ? $"Found: {result2}" : "Product not found");
    }
}
