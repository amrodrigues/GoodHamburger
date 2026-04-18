using GoodHamburger.Api.Models;
using GoodHamburger.Api.Services;
using Microsoft.EntityFrameworkCore;
using GoodHamburger.Api.Data;
using Xunit;

namespace GoodHamburger.Tests;

public class OrderServiceTests
{
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        var options = new DbContextOptionsBuilder<OrdersDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var db = new OrdersDbContext(options);
        // Ensure seeded menu exists
        db.Database.EnsureCreated();

        _service = new OrderService(db);
    }

    [Fact]
    public async Task CreateOrder_FullCombo_ShouldApply20PercentDiscount()
    {
        // Arrange: Sandwich (5.00) + Side (2.00) + Drink (2.50) = 9.50
        var request = new CreateOrderRequest { ItemIds = new List<int> { 1, 4, 5 } };

        // Act
        var result = await _service.CreateOrderAsync(request);

        // Assert: 9.50 * 0.8 = 7.60
        Assert.Equal(9.50m, result.Subtotal);
        Assert.Equal(1.90m, result.Discount);
        Assert.Equal(7.60m, result.Total);
    }

    [Fact]
    public async Task CreateOrder_SandwichAndDrink_ShouldApply15PercentDiscount()
    {
        // Arrange: Sandwich (5.00) + Drink (2.50) = 7.50
        var request = new CreateOrderRequest { ItemIds = new List<int> { 1, 5 } };

        // Act
        var result = await _service.CreateOrderAsync(request);

        // Assert: 7.50 * 0.15 = 1.125 -> 1.125
        Assert.Equal(7.50m, result.Subtotal);
        Assert.Equal(1.125m, result.Discount);
        Assert.Equal(6.375m, result.Total);
    }

    [Fact]
    public async Task CreateOrder_SandwichAndSide_ShouldApply10PercentDiscount()
    {
        // Arrange: Sandwich (5.00) + Side (2.00) = 7.00
        var request = new CreateOrderRequest { ItemIds = new List<int> { 1, 4 } };

        // Act
        var result = await _service.CreateOrderAsync(request);

        // Assert: 7.00 * 0.10 = 0.70
        Assert.Equal(7.00m, result.Subtotal);
        Assert.Equal(0.70m, result.Discount);
        Assert.Equal(6.30m, result.Total);
    }

    [Fact]
    public async Task CreateOrder_DuplicateItems_ShouldThrowArgumentException()
    {
        // Arrange: Two sandwiches
        var request = new CreateOrderRequest { ItemIds = new List<int> { 1, 2 } };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _service.CreateOrderAsync(request));
        Assert.Contains("Cada pedido pode conter apenas um sanduíche", exception.Message);
    }

    [Fact]
    public async Task CreateOrder_InvalidItemId_ShouldThrowArgumentException()
    {
        // Arrange: ID 99 doesn't exist
        var request = new CreateOrderRequest { ItemIds = new List<int> { 99 } };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _service.CreateOrderAsync(request));
        Assert.Contains("não existe no cardápio", exception.Message);
    }
}
