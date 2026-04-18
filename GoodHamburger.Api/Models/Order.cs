using System.ComponentModel.DataAnnotations;

namespace GoodHamburger.Api.Models;

public class Order
{
    public Guid Id { get; set; }
    // Store ItemIds as JSON string in the database
    public List<int> ItemIds { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateOrderRequest
{
    [Required]
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
