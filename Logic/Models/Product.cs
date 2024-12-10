namespace Name
{
    
}
public abstract class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public abstract string GetProductType();
}

public class Electronics : Product
{
    public override string GetProductType() => "Electronics";
}

public class Accessories : Product
{
    public override string GetProductType() => "Accessories";
}
