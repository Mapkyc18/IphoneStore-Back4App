using System;
using System.Collections.Generic;
using System.Linq;
using testing_final.Logic.Models;
using testing_final.Logic.Interfaces;

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
        var orders = _orders.Where(o => !o.IsFulfilled).ToList();

        foreach (var order in orders)
        {
            // Populate items for each order
            order.Items = GetItemsByOrderId(order.OrderId);
        }

        return orders;
    }

    public void FulfillOrder(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            order.IsFulfilled = true;
        }
    }

    public List<Order> GetOrdersByDate(DateTime date)
    {
        var orders = _orders.Where(o => o.OrderDate.Date == date.Date).ToList();

        foreach (var order in orders)
        {
            // Populate items for each order
            order.Items = GetItemsByOrderId(order.OrderId);
        }

        return orders;
    }

    public List<Item> GetItemsByOrderId(int orderId)
    {
        return _items.Where(i => i.OrderId == orderId).ToList();
    }
}
