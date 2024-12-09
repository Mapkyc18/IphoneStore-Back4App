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
        // Fetch pending orders from the database
        var orders = _database.GetPendingOrders();

        foreach (var order in orders)
        {
            // Populate items for each order
            order.Items = _database.GetItemsByOrderId(order.OrderId);
        }

        return orders;
    }

    public void FulfillOrder(int orderId)
    {
        _database.FulfillOrder(orderId);
    }

    public List<Order> GetOrdersByDate(DateTime date)
    {
        var orders = _database.GetOrdersByDate(date);

        foreach (var order in orders)
        {
            // Populate items for each order
            order.Items = _database.GetItemsByOrderId(order.OrderId);
        }

        return orders;
    }

    public Order? GetOrderDetails(int orderId)
    {
        // Retrieve the order from the database
        var order = _database.GetPendingOrders().FirstOrDefault(o => o.OrderId == orderId);

        if (order != null)
        {
            // Fetch associated items for the order
            order.Items = _database.GetItemsByOrderId(order.OrderId);
            Console.WriteLine($"Order ID: {order.OrderId}, TotalAmount: {order.TotalAmount:C}");
            foreach (var item in order.Items)
            {
                Console.WriteLine($"Item: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price:C}");
            }
        }

        return order;
    }
}
