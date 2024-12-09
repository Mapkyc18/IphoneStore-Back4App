using Xunit;
using FluentAssertions;
using testing_final.Logic.Models;
using testing_final.Tests.Mocks;

namespace testing_final.Tests.LogicTests;

public class DatabaseTests
{
    [Fact]
    public void SaveOrder_ShouldAddOrderToDatabase()
    {
        // Arrange
        var database = new InMemoryDatabase();

        var order = new Order
        {
            OrderDate = DateTime.Now,
            Items = new List<Item> { new Item { Name = "iPhone", Price = 999.99m, Quantity = 1 } }
        };

        // Act
        database.SaveOrder(order);

        // Assert
        var savedOrder = database.GetPendingOrders().FirstOrDefault();
        savedOrder.Should().NotBeNull();
        savedOrder.Items.Should().HaveCount(1);
    }

    [Fact]
    public void FulfillOrder_ShouldUpdateOrderState()
    {
        // Arrange
        var database = new InMemoryDatabase();

        var order = new Order
        {
            OrderDate = DateTime.Now,
            Items = new List<Item> { new Item { Name = "iPhone", Price = 999.99m, Quantity = 1 } }
        };
        database.SaveOrder(order);

        // Act
        database.FulfillOrder(order.OrderId);

        // Assert
        var pendingOrders = database.GetPendingOrders();
        pendingOrders.Should().BeEmpty();
    }
}
