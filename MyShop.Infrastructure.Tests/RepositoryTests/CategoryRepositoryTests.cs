using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyShop.Core.Models;
using MyShop.Infrastructure.Databases;
using MyShop.Infrastructure.Repositories;
using Xunit;

namespace MyShop.Infrastructure.Tests.RepositoryTests;

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
        //Arrange
        var repository = new CategoryRepository(_context);
        
        //Act
        await repository.Add(_categories[0]);
        await repository.Save();
        
        //Assert
        Assert.Single(_context.Categories);
    }

    [Fact]
    public async Task GetCategory_NotNull_Success()
    {
        //Arrange
        var repository = new CategoryRepository(_context);
        var category = _categories[0];
        await repository.Add(category);
        await repository.Save();

        //Act
        var categoryFromRepository = repository.GetById(category.Id);

        //Assert
        Assert.NotNull(categoryFromRepository);
    }
}