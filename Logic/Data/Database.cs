using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using testing_final.Logic.Interfaces;
using testing_final.Logic.Models;

namespace testing_final.Logic.Data;

public class Database : IDatabase
{
    private readonly SQLiteConnection _db;

    public Database(string dbPath)
    {
        if (string.IsNullOrWhiteSpace(dbPath))
        {
            throw new ArgumentException("Database path cannot be null or empty.", nameof(dbPath));
        }

        Console.WriteLine($"Initializing database at: {dbPath}");
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath) ?? throw new ArgumentException("Invalid database path.", nameof(dbPath)));

        _db = new SQLiteConnection(dbPath);
        _db.CreateTable<Order>();
        _db.CreateTable<Item>();
    }

    public void SaveOrder(Order order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        // Insert the order and get the generated OrderId
        _db.Insert(order);
        order.OrderId = _db.ExecuteScalar<int>("SELECT last_insert_rowid()");

        // Save the associated items
        foreach (var item in order.Items)
        {
            item.OrderId = order.OrderId; // Link items to the saved order
            _db.Insert(item);
        }
    }


    public List<Order> GetPendingOrders()
    {
        var orders = _db.Table<Order>().Where(o => !o.IsFulfilled).ToList();

        foreach (var order in orders)
        {
            // Populate Items for each order
            order.Items = GetItemsByOrderId(order.OrderId);
        }

        return orders;
    }

    public void FulfillOrder(int orderId)
    {
        var order = _db.Find<Order>(orderId);
        if (order != null)
        {
            order.IsFulfilled = true;
            _db.Update(order);
        }
    }

    public List<Order> GetOrdersByDate(DateTime date)
    {
        var startOfDay = date.Date; // Midnight of the specified day
        var endOfDay = startOfDay.AddDays(1); // Midnight of the next day

        var orders = _db.Table<Order>()
                        .Where(o => o.OrderDate >= startOfDay && o.OrderDate < endOfDay)
                        .ToList();

        foreach (var order in orders)
        {
            // Populate Items for each order
            order.Items = GetItemsByOrderId(order.OrderId);
        }

        return orders;
    }

    public List<Item> GetItemsByOrderId(int orderId)
    {
        return _db.Table<Item>().Where(i => i.OrderId == orderId).ToList();
    }
}
