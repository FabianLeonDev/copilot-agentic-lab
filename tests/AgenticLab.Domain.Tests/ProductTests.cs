using AgenticLab.Domain;

namespace AgenticLab.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void Constructor_CreatesProductWithGeneratedIdAndInitialValues()
    {
        var product = new Product("Widget", 5);

        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Equal("Widget", product.Name);
        Assert.Equal(5, product.Stock);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_RejectsEmptyOrWhitespaceName(string name)
    {
        Assert.Throws<ArgumentException>(() => new Product(name, 0));
    }

    [Fact]
    public void Constructor_RejectsNullName()
    {
        Assert.Throws<ArgumentException>(() => new Product(null!, 0));
    }

    [Fact]
    public void Constructor_RejectsNegativeInitialStock()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Product("Widget", -1));
    }

    [Fact]
    public void AddStock_IncreasesStock()
    {
        var product = new Product("Widget", 5);

        product.AddStock(3);

        Assert.Equal(8, product.Stock);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AddStock_RejectsNonPositiveQuantityAndPreservesStock(int quantity)
    {
        var product = new Product("Widget", 5);

        Assert.Throws<ArgumentOutOfRangeException>(() => product.AddStock(quantity));

        Assert.Equal(5, product.Stock);
    }

    [Fact]
    public void AddStock_RejectsOverflowAndPreservesStock()
    {
        var product = new Product("Widget", int.MaxValue);

        Assert.Throws<InvalidOperationException>(() => product.AddStock(1));

        Assert.Equal(int.MaxValue, product.Stock);
    }

    [Fact]
    public void RemoveStock_DecreasesStock()
    {
        var product = new Product("Widget", 5);

        product.RemoveStock(3);

        Assert.Equal(2, product.Stock);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void RemoveStock_RejectsNonPositiveQuantityAndPreservesStock(int quantity)
    {
        var product = new Product("Widget", 5);

        Assert.Throws<ArgumentOutOfRangeException>(() => product.RemoveStock(quantity));

        Assert.Equal(5, product.Stock);
    }

    [Fact]
    public void RemoveStock_RejectsQuantityAboveAvailableStockAndPreservesStock()
    {
        var product = new Product("Widget", 5);

        Assert.Throws<InvalidOperationException>(() => product.RemoveStock(6));

        Assert.Equal(5, product.Stock);
    }
}
