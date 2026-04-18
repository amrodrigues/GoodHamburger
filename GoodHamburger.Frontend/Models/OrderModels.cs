namespace GoodHamburger.Frontend.Models;

public enum ItemType
{
    Sandwich,
    Side,
    Drink
}

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ItemType Type { get; set; }
}

public class CreateOrderRequest
{
    public List<int> ItemIds { get; set; } = new();
}

public class OrderResponse
{
    public Guid Id { get; set; }
    public List<MenuItem> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}
