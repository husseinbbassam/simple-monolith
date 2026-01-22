using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogApi.Data;
using ProductCatalogApi.Models;

namespace ProductCatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/Order/PlaceOrder
    [HttpPost("PlaceOrder")]
    public async Task<ActionResult<Order>> PlaceOrder(PlaceOrderRequest request)
    {
        // 1. Check if the product exists
        var product = await _context.Products.FindAsync(request.ProductId);
        if (product == null)
        {
            return NotFound($"Product with ID {request.ProductId} not found.");
        }

        // 2. Check if there's enough stock
        if (product.StockQuantity < 1)
        {
            return BadRequest($"Product '{product.Name}' is out of stock.");
        }

        // 3. Deduct stock from the Product table
        product.StockQuantity -= 1;

        // 4. Create and save the Order
        var order = new Order
        {
            CustomerName = request.CustomerName,
            ProductId = request.ProductId,
            OrderDate = DateTime.UtcNow
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // 5. Simulate sending a confirmation email
        Console.WriteLine($"[EMAIL SIMULATION] Sending confirmation email to customer: {request.CustomerName}");
        Console.WriteLine($"[EMAIL SIMULATION] Order #{order.Id} placed for product: {product.Name}");

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    // GET: api/Order/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    // GET: api/Order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders
            .Include(o => o.Product)
            .ToListAsync();
    }
}

public class PlaceOrderRequest
{
    public string CustomerName { get; set; } = string.Empty;
    public int ProductId { get; set; }
}
