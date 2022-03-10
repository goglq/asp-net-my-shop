using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyShop.HttpApiServer.Infrastructure;
using MyShop.Infrastructure.Repositories;
using MyShop.Models;
using Xunit;

namespace MyShop.Tests;

public class ProductRepositoryTests
{
    private readonly AppDbContext _context;

    private readonly IReadOnlyList<Product> _products;

    public ProductRepositoryTests()
    {
        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        IReadOnlyList<Category> categories = Enumerable.Range(0, 10)
            .Select(i => new Category()
            {
                Id = Guid.NewGuid(),
                Name = $"Test Category {i + 1}"
            }).ToList();

        _products = Enumerable.Range(0, 10)
            .Select(i => new Product()
            {
                Id = Guid.NewGuid(),
                Name = $"Test {i}",
                Description = "Test description",
                Price = 100,
                CategoryId = categories[0].Id,
                ImageUrl = "https://via.placeholder.com/200x200"
            }).ToList();

        _context = new AppDbContext(dbOptions.Options);
        _context.Categories.AddRange(categories);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddProduct_Single()
    {
        var repository = new ProductRepository(_context);

        await repository.Add(_products[0]);
        await repository.Save();

        Assert.Single(_context.Products);
    }

    [Fact]
    public async Task GetAllProducts_NotEmpty_EqualCount()
    {
        var repository = new ProductRepository(_context);
        _context.Products.AddRange(_products);
        await _context.SaveChangesAsync();

        var products = repository.GetAll();
        Assert.NotEmpty(products);
        Assert.Equal(_products.Count, products.Count());
    }

    [Fact]
    public async Task GetProductById_NotNull()
    {
        var repository = new ProductRepository(_context);
        var product = _products[0];

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var productFromRepository = repository.GetById(product.Id);
        
        Assert.NotNull(productFromRepository);
    }
}