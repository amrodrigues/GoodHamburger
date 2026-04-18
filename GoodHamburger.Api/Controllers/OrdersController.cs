using GoodHamburger.Api.Models;
using GoodHamburger.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("menu")]
    public IActionResult GetMenu()
    {
        var menu = _orderService.GetMenu();
        return Ok(menu);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var orders = _orderService.GetAllOrdersAsync().GetAwaiter().GetResult();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var order = _orderService.GetOrderByIdAsync(id).GetAwaiter().GetResult();
        if (order == null) return NotFound("Pedido não encontrado.");
        return Ok(order);
    }

    [HttpPost]
    public IActionResult Create(CreateOrderRequest request)
    {
        try
        {
            var order = _orderService.CreateOrderAsync(request).GetAwaiter().GetResult();
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, CreateOrderRequest request)
    {
        try
        {
            var order = _orderService.UpdateOrderAsync(id, request).GetAwaiter().GetResult();
            return Ok(order);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (!_orderService.DeleteOrderAsync(id).GetAwaiter().GetResult()) return NotFound("Pedido não encontrado.");
        return NoContent();
    }
}
