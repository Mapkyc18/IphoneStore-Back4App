using System.Text.Json;
using SQLite;

namespace testing_final.Logic.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal SalesTax { get; set; }
    public bool IsFulfilled { get; set; }

    // Store Items as a JSON string in the database
    public string ItemsJson
    {
        get => JsonSerializer.Serialize(Items);
        set => Items = string.IsNullOrEmpty(value) ? new List<Item>() : JsonSerializer.Deserialize<List<Item>>(value);
    }

    [Ignore] // Prevent SQLite from trying to map this directly
    public List<Item> Items { get; set; } = new();
}
