namespace TestingFinal.Logic.Helpers
{
    public static class Utility
    {
        //  calculate sales tax
        public static decimal CalculateSalesTax(decimal subtotal, decimal taxRate)
        {
            return subtotal * taxRate;
        }
    }
}
