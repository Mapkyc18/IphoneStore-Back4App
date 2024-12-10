using testing_final.Logic.Models;
using testing_final.Logic.Interfaces;

namespace testing_final.Logic.Services;

public class OrderManager : IOrderManager
{
    private readonly IDatabase _database;

    public OrderManager(IDatabase database)
    {
        _database = database;
    }

    public static class ProductCatalog
    {
        public static readonly List<Item> Products = new()
        {
            new Item { Name = "iPhone 16", Price = 1099 },
            new Item { Name = "iPhone 15", Price = 899 },
            new Item { Name = "iPhone 14", Price = 799 },
            new Item { Name = "iPhone 13", Price = 699 },
            new Item { Name = "iPhone 12", Price = 599 }
        };
    }

    public void AddOrder(Order order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        order.CalculateTotal(0.07m); // Calculate tax and total
        _database.SaveOrder(order);

        OrderAdded?.Invoke(this, order); // Raise event for UI updates
    }

    public List<Order> GetPendingOrders()
    {
        var orders = _database.GetPendingOrders();

        foreach (var order in orders)
        {
            order.Items = _database.GetItemsByOrderId(order.OrderId);
        }

        return orders;
    }

    public void FulfillOrder(int orderId)
    {
        _database.FulfillOrder(orderId);
        OrderFulfilled?.Invoke(this, orderId); // Raise event for UI updates
    }

    public List<Order> GetOrdersByDate(DateTime date)
    {
        var orders = _database.GetOrdersByDate(date);

        foreach (var order in orders)
        {
            order.Items = _database.GetItemsByOrderId(order.OrderId);
        }

        return orders;
    }

    public Order? GetOrderDetails(int orderId)
    {
        var order = _database.GetPendingOrders().FirstOrDefault(o => o.OrderId == orderId);

        if (order != null)
        {
            order.Items = _database.GetItemsByOrderId(order.OrderId);
        }

        return order;
    }

    public event EventHandler<Order>? OrderAdded; // Event for order added
    public event EventHandler<int>? OrderFulfilled; // Event for order fulfilled
}
