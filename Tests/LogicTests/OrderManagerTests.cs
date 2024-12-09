using Xunit;
using FluentAssertions;
using testing_final.Logic.Models;
using testing_final.Logic.Services;
using testing_final.Tests.Mocks;

namespace testing_final.Tests.LogicTests;

public class OrderManagerTests
{
    [Fact]
    public void AddOrder_ShouldSaveOrderToDatabase()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var orderManager = new OrderManager(database);

        var order = new Order
        {
            OrderDate = DateTime.Now,
            Items = new List<Item>
            {
                new Item { Name = "iPhone 15", Price = 1199.99m, Quantity = 1 },
                new Item { Name = "iPhone Case", Price = 49.99m, Quantity = 2 }
            }
        };

        // Act
        orderManager.AddOrder(order);

        // Assert
        var savedOrders = orderManager.GetPendingOrders();
        savedOrders.Should().HaveCount(1);
        savedOrders.First().Items.Should().HaveCount(2);
    }

    [Fact]
    public void FulfillOrder_ShouldMarkOrderAsFulfilled()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var orderManager = new OrderManager(database);

        var order = new Order
        {
            OrderDate = DateTime.Now,
            Items = new List<Item> { new Item { Name = "iPhone", Price = 999.99m, Quantity = 1 } }
        };
        orderManager.AddOrder(order);

        // Act
        orderManager.FulfillOrder(order.OrderId);

        // Assert
        var pendingOrders = orderManager.GetPendingOrders();
        pendingOrders.Should().BeEmpty();
    }
}
