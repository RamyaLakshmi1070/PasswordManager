using Microsoft.EntityFrameworkCore;
using PasswordManager.Data.Models; // change to your actual DbContext namespace

public static class TestHelper
{
    public static PasswordManagerDbContext GetInMemoryDbContext(string dbName = "TestDb")
    {
        var options = new DbContextOptionsBuilder<PasswordManagerDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new PasswordManagerDbContext(options);

        context.Database.EnsureCreated();

        return context;
    }
}
