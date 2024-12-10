namespace testing_final.Logic.Utilities;

public static class TaxCalculator
{
    public static decimal CalculateTax(decimal subtotal, decimal taxRate = 0.07m)
    {
        return subtotal * taxRate;
    }
    public static string FormatPrice(decimal price)
    {
        return price.ToString("C");
    }

}
