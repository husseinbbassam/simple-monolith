namespace ProductCatalogApi.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public DateTime OrderDate { get; set; }
    
    // Navigation property
    public Product? Product { get; set; }
}
