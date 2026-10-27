namespace AgenticLab.Domain;

/// <summary>A product whose stock can be increased or decreased with validation.</summary>
/// <remarks>
/// Assumptions: IDs are generated as non-empty <see cref="Guid"/> values; names must contain
/// a non-whitespace character; stock and quantities are non-negative/positive integers.
/// Invalid names throw <see cref="ArgumentException"/>, negative initial stock and
/// non-positive quantities throw <see cref="ArgumentOutOfRangeException"/>, and removing
/// unavailable stock (or exceeding the integer stock limit) throws
/// <see cref="InvalidOperationException"/>. Failed stock operations leave stock unchanged.
/// </remarks>
public sealed class Product
{
    /// <summary>Creates a product with a generated ID.</summary>
    /// <param name="name">The product name; cannot be null, empty, or whitespace.</param>
    /// <param name="stock">The initial stock; cannot be negative.</param>
    /// <exception cref="ArgumentException">The name is null, empty, or whitespace.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The initial stock is negative.</exception>
    public Product(string name, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product name cannot be empty or whitespace.", nameof(name));
        }

        if (stock < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(stock), stock, "Initial stock cannot be negative.");
        }

        Id = Guid.NewGuid();
        Name = name;
        Stock = stock;
    }

    /// <summary>Gets the generated unique identifier.</summary>
    public Guid Id { get; }

    /// <summary>Gets the product name.</summary>
    public string Name { get; }

    /// <summary>Gets the current stock quantity.</summary>
    public int Stock { get; private set; }

    /// <summary>Adds a positive quantity to stock.</summary>
    /// <param name="quantity">The positive quantity to add.</param>
    /// <exception cref="ArgumentOutOfRangeException">The quantity is not positive.</exception>
    /// <exception cref="InvalidOperationException">Adding the quantity would exceed the integer stock limit.</exception>
    public void AddStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity must be positive.");
        }

        if (quantity > int.MaxValue - Stock)
        {
            throw new InvalidOperationException("Adding this quantity would exceed the maximum stock value.");
        }

        Stock += quantity;
    }

    /// <summary>Removes a positive quantity from stock.</summary>
    /// <param name="quantity">The positive quantity to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">The quantity is not positive.</exception>
    /// <exception cref="InvalidOperationException">The quantity exceeds available stock.</exception>
    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity must be positive.");
        }

        if (quantity > Stock)
        {
            throw new InvalidOperationException("Cannot remove more stock than is available.");
        }

        Stock -= quantity;
    }
}
