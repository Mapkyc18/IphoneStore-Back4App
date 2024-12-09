using System.Collections.Generic;
using System.Linq;
using testing_final.Logic.Models;
using testing_final.Logic.Data;

namespace testing_final.Tests.Mocks;

public class InMemoryDatabase : IDatabase
{
    private readonly List<Order> _orders = new();
    private readonly List<Item> _items = new();

    public void SaveOrder(Order order)
    {
        order.OrderId = _orders.Count + 1;
        _orders.Add(order);
        foreach (var item in order.Items)
        {
            item.ItemId = _items.Count + 1;
            item.OrderId = order.OrderId;
            _items.Add(item);
        }
    }

    public List<Order> GetPendingOrders()
    {
        return _orders.Where(o => !o.IsFulfilled).ToList();
    }

    public void FulfillOrder(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            order.IsFulfilled = true;
        }
    }
}
