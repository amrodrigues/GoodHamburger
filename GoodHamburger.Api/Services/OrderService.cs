using GoodHamburger.Api.Models;
using GoodHamburger.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Api.Services;

public interface IOrderService
{
    IEnumerable<MenuItem> GetMenu();
    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request);
    Task<IEnumerable<OrderResponse>> GetAllOrdersAsync();
    Task<OrderResponse?> GetOrderByIdAsync(Guid id);
    Task<OrderResponse> UpdateOrderAsync(Guid id, CreateOrderRequest request);
    Task<bool> DeleteOrderAsync(Guid id);
}

public class OrderService : IOrderService
{
    private readonly OrdersDbContext _db;

    public OrderService(OrdersDbContext db)
    {
        _db = db;
    }

    public IEnumerable<MenuItem> GetMenu() => _db.MenuItems.ToList();

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        var items = ValidateAndGetItems(request.ItemIds);
        var order = CalculateOrder(items);
        order.Id = Guid.NewGuid();
        order.CreatedAt = DateTime.UtcNow;

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return MapToResponse(order);
    }

    public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync()
    {
        var orders = await _db.Orders.ToListAsync();
        return orders.Select(MapToResponse);
    }

    public async Task<OrderResponse?> GetOrderByIdAsync(Guid id)
    {
        var order = await _db.Orders.FindAsync(id);
        return order != null ? MapToResponse(order) : null;
    }

    public async Task<OrderResponse> UpdateOrderAsync(Guid id, CreateOrderRequest request)
    {
        var existingOrder = await _db.Orders.FindAsync(id);
        if (existingOrder == null) throw new KeyNotFoundException("Pedido não encontrado.");

        var items = ValidateAndGetItems(request.ItemIds);
        var updatedOrder = CalculateOrder(items);

        existingOrder.ItemIds = updatedOrder.ItemIds;
        existingOrder.Subtotal = updatedOrder.Subtotal;
        existingOrder.Discount = updatedOrder.Discount;
        existingOrder.Total = updatedOrder.Total;

        _db.Orders.Update(existingOrder);
        await _db.SaveChangesAsync();

        return MapToResponse(existingOrder);
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order == null) return false;
        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();
        return true;
    }

    private List<MenuItem> ValidateAndGetItems(List<int> itemIds)
    {
        var items = new List<MenuItem>();
        var typesCount = new Dictionary<ItemType, int>();

        foreach (var id in itemIds)
        {
            var item = _db.MenuItems.FirstOrDefault(m => m.Id == id);
            if (item == null) throw new ArgumentException($"Item com ID {id} não existe no cardápio.");

            if (!typesCount.ContainsKey(item.Type))
                typesCount[item.Type] = 0;

            typesCount[item.Type]++;

            if (typesCount[item.Type] > 1)
                throw new ArgumentException($"Cada pedido pode conter apenas um {GetTypeName(item.Type)}.");

            items.Add(item);
        }

        return items;
    }

    private Order CalculateOrder(List<MenuItem> items)
    {
        decimal subtotal = items.Sum(i => i.Price);
        decimal discountPercentage = 0;

        bool hasSandwich = items.Any(i => i.Type == ItemType.Sandwich);
        bool hasSide = items.Any(i => i.Type == ItemType.Side);
        bool hasDrink = items.Any(i => i.Type == ItemType.Drink);

        if (hasSandwich && hasSide && hasDrink)
            discountPercentage = 0.20m;
        else if (hasSandwich && hasDrink)
            discountPercentage = 0.15m;
        else if (hasSandwich && hasSide)
            discountPercentage = 0.10m;

        decimal discount = subtotal * discountPercentage;
        decimal total = subtotal - discount;

        return new Order
        {
            ItemIds = items.Select(i => i.Id).ToList(),
            Subtotal = subtotal,
            Discount = discount,
            Total = total
        };
    }

    private OrderResponse MapToResponse(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            Items = order.ItemIds.Select(id => _db.MenuItems.First(m => m.Id == id)).ToList(),
            Subtotal = order.Subtotal,
            Discount = order.Discount,
            Total = order.Total,
            CreatedAt = order.CreatedAt
        };
    }

    private string GetTypeName(ItemType type) => type switch
    {
        ItemType.Sandwich => "sanduíche",
        ItemType.Side => "acompanhamento (batata)",
        ItemType.Drink => "bebida (refrigerante)",
        _ => "item"
    };
}
