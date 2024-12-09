using testing_final.Logic.Models;

namespace testing_final.Logic.Interfaces;

public interface IDatabase
{
    void SaveOrder(Order order);
    List<Order> GetPendingOrders();
    void FulfillOrder(int orderId);
    List<Order> GetOrdersByDate(DateTime date);
    List<Item> GetItemsByOrderId(int orderId);

}