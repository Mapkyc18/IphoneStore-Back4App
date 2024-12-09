using SQLite;
using testing_final.Logic.Interfaces;
using testing_final.Logic.Models;

namespace testing_final.Logic.Data;

public class Database : IDatabase
{
    private readonly SQLiteConnection _db;

    public Database(string dbPath)
    {
        _db = new SQLiteConnection(dbPath);
        _db.CreateTable<Order>();
        _db.CreateTable<Item>();
    }

    public void SaveOrder(Order order)
    {
        _db.Insert(order);
        foreach (var item in order.Items)
        {
            item.OrderId = order.OrderId;
            _db.Insert(item);
        }
    }

    public List<Order> GetPendingOrders()
    {
        return _db.Table<Order>().Where(o => !o.IsFulfilled).ToList();
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
        return _db.Table<Order>().Where(o => o.OrderDate.Date == date.Date).ToList();
    }
}
