using SQLite;
using System.Collections.Generic;
using testing_final.Logic.Models;

public class Order
{
    [PrimaryKey, AutoIncrement]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal SalesTax { get; set; }
    public bool IsFulfilled { get; set; }

    [Ignore]
    public List<Item> Items { get; set; } = new();

    public void CalculateTotal(decimal taxRate)
    {
        decimal subtotal = Items.Sum(item => item.Price * item.Quantity);
        SalesTax = subtotal * taxRate;
        TotalAmount = subtotal + SalesTax;
    }
}
