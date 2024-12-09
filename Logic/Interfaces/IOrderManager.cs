using testing_final.Logic.Models;

namespace testing_final.Logic.Interfaces;

public interface IOrderManager
{
    void AddOrder(Order order);
    List<Order> GetPendingOrders();
    void FulfillOrder(int orderId);
    List<Order> GetOrdersByDate(DateTime date);
    Order GetOrderDetails(int orderId);
}