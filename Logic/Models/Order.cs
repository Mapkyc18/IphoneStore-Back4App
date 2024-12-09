namespace testing_final.Logic.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal SalesTax { get; set; }
    public bool IsFulfilled { get; set; }
    public List<Item> Items { get; set; } = new();

    public decimal CalculateTotal(decimal taxRate)
    {
        SalesTax = Items.Sum(item => item.Price * item.Quantity) * taxRate;
        TotalAmount = Items.Sum(item => item.Price * item.Quantity) + SalesTax;
        return TotalAmount;
    }
}
