using System.Linq;

namespace testing_final.Logic.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal SalesTax { get; set; }
    public bool IsFulfilled { get; set; }

    public string ItemsJson
    {
        get => System.Text.Json.JsonSerializer.Serialize(Items);
        set => Items = string.IsNullOrEmpty(value) ? new List<Item>() : System.Text.Json.JsonSerializer.Deserialize<List<Item>>(value);
    }

    [SQLite.Ignore]
    public List<Item> Items { get; set; } = new();

    public void CalculateTotal(decimal taxRate)
    {
        decimal subtotal = Items.Sum(item => item.Price * item.Quantity);
        SalesTax = subtotal * taxRate;
        TotalAmount = subtotal + SalesTax;
    }
}
