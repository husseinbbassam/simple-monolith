# Simple Monolith Project

A .NET 8 Web API for learning Microservices concepts. This is a Product Catalog & Orders system built as a monolith to serve as a starting point for understanding microservices architecture.

## Features

- **Product Management**: Full CRUD operations for products
- **Order Management**: Place orders with automatic stock deduction
- **SQLite Database**: Easy local setup with Entity Framework Core
- **Email Simulation**: Console logging to simulate order confirmation emails

## Project Structure

```
ProductCatalogApi/
├── Models/
│   ├── Product.cs          # Product entity (Id, Name, Price, StockQuantity)
│   └── Order.cs            # Order entity (Id, CustomerName, ProductId, OrderDate)
├── Controllers/
│   ├── ProductController.cs # CRUD endpoints for products
│   └── OrderController.cs   # Order placement endpoint
├── Data/
│   └── ApplicationDbContext.cs # EF Core database context
└── Program.cs               # Application configuration
```

## Entities

### Product
- `Id` (int): Primary key
- `Name` (string): Product name
- `Price` (decimal): Product price
- `StockQuantity` (int): Available stock

### Order
- `Id` (int): Primary key
- `CustomerName` (string): Name of the customer
- `ProductId` (int): Foreign key to Product
- `OrderDate` (DateTime): When the order was placed

## API Endpoints

### Products

- `GET /api/Product` - Get all products
- `GET /api/Product/{id}` - Get a specific product
- `POST /api/Product` - Create a new product
- `PUT /api/Product/{id}` - Update a product
- `DELETE /api/Product/{id}` - Delete a product

### Orders

- `POST /api/Order/PlaceOrder` - Place a new order
  - Checks if product exists
  - Verifies stock availability
  - Deducts stock quantity
  - Saves the order
  - Logs email confirmation simulation
- `GET /api/Order` - Get all orders
- `GET /api/Order/{id}` - Get a specific order

## Getting Started

### Prerequisites

- .NET 8 SDK or later

### Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run the application:

```bash
dotnet run
```

The API will start on `http://localhost:5108` (or the port specified in `launchSettings.json`).

### Example Usage

**Create a Product:**
```bash
curl -X POST http://localhost:5108/api/Product \
  -H "Content-Type: application/json" \
  -d '{"name":"Gaming Laptop","price":1299.99,"stockQuantity":10}'
```

**Get All Products:**
```bash
curl -X GET http://localhost:5108/api/Product
```

**Place an Order:**
```bash
curl -X POST http://localhost:5108/api/Order/PlaceOrder \
  -H "Content-Type: application/json" \
  -d '{"customerName":"John Doe","productId":1}'
```

When an order is placed, you'll see console output like:
```
[EMAIL SIMULATION] Sending confirmation email to customer: John Doe
[EMAIL SIMULATION] Order #1 placed for product: Gaming Laptop
```

## Database

The application uses SQLite with Entity Framework Core. The database file (`productcatalog.db`) is automatically created in the project root on first run.

## Future Enhancements

This monolithic application can be evolved into microservices by:
- Separating Product Catalog and Order services
- Implementing message queues for inter-service communication
- Adding API Gateway
- Implementing distributed transactions
- Adding authentication and authorization
