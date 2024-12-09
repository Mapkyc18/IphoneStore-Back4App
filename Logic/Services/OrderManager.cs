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

    public Order GetOrderDetails(int orderId)
    {
        var orders = _database.GetPendingOrders();
        return orders.FirstOrDefault(o => o.OrderId == orderId);
    }
}
