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

public class CategoryRepositoryTests
{
    private readonly AppDbContext _context;

    private readonly IReadOnlyList<Category> _categories;

    public CategoryRepositoryTests()
    {
        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new AppDbContext(dbOptions.Options);
        
        _categories = Enumerable.Range(0, 10)
            .Select(i => new Category()
            {
                Id = Guid.NewGuid(),
                Name = $"Test Category {i + 1}"
            }).ToList();
    }

    [Fact]
    public async Task AddCategory_Single_Success()
    {
        var repository = new CategoryRepository(_context);

        await repository.Add(_categories[0]);
        await repository.Save();

        Assert.Single(_context.Categories);
    }

    [Fact]
    public async Task GetCategory_NotNull_Success()
    {
        var repository = new CategoryRepository(_context);

        var category = _categories[0];
        
        await repository.Add(category);
        await repository.Save();

        var categoryFromRepository = repository.GetById(category.Id);

        Assert.NotNull(categoryFromRepository);
    }
}