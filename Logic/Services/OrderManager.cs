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

    public void AddOrder(Order order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        // Calculate total before saving
        order.CalculateTotal(0.07m); // 7% sales tax
        _database.SaveOrder(order);
    }

    public List<Order> GetPendingOrders()
    {
        return _database.GetPendingOrders();
    }

    public void FulfillOrder(int orderId)
    {
        _database.FulfillOrder(orderId);
    }

    public List<Order> GetOrdersByDate(DateTime date)
    {
        return _database.GetOrdersByDate(date);
    }

    public Order? GetOrderDetails(int orderId)
    {
        return GetPendingOrders().FirstOrDefault(o => o.OrderId == orderId);
    }
}
