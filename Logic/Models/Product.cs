namespace TestingFinal.Logic.Models
{
    public abstract class Product
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class Electronics : Product { }
    public class Accessories : Product { }
}
