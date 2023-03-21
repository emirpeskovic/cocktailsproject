using CocktailsProject;
using CocktailsProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var databaseManager = new DatabaseManager(configuration);
databaseManager.UseContext(context =>
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
});

var entityManager = new EntityManager(databaseManager);

var ingredients = new List<Ingredient>
{
    new Ingredient { Name = "Lime Juice" },
    new Ingredient { Name = "Triple Sec" },
    new Ingredient { Name = "Tequila" },
    new Ingredient { Name = "Salt Rim" },
    new Ingredient { Name = "Crushed Ice" },
    new Ingredient { Name = "Lime Segment" },
    new Ingredient { Name = "Dark Rum" },
    new Ingredient { Name = "Orange Curacao" },
    new Ingredient { Name = "Almond Syrup" },
    new Ingredient { Name = "Lime Section" },
    new Ingredient { Name = "Maraschino Cherry" },
};

foreach (var ingredient in ingredients)
{
    entityManager.Add(ingredient);
}

var margarita = new Drink
{
    Name = "Margarita",
    Recipe = new Recipe
    {
        Ingredients = new Dictionary<Ingredient, double>
        {
            { ingredients[0], 60 },
            { ingredients[1], 30 },
            { ingredients[2], 60 },
            { ingredients[3], 1 },
            { ingredients[4], 1 },
            { ingredients[5], 1 },
        }
    }
};

entityManager.Add(margarita);

var maitai = new Drink
{
    Name = "Mai Tai",
    Recipe = new Recipe
    {
        Ingredients = new Dictionary<Ingredient, double>
        {
            { ingredients[6], 50 },
            { ingredients[7], 15 },
            { ingredients[0], 10 },
            { ingredients[8], 60 },
            { ingredients[9], 1 },
            { ingredients[10], 1 },
            { ingredients[5], 1 },
        }
    }
};

entityManager.Add(maitai);

var drinks = entityManager.Entity<Drink>()
    .Include(d => d.Recipe)
    .ThenInclude(r => r.Ingredients)
    .ToList();

foreach (var drink in drinks)
{
    Console.WriteLine(drink.Name);
    
    foreach (var ingredient in drink.Recipe.Ingredients)
    {
        var unit = ingredient.Value > 1 ? "ml" : "part";
        Console.WriteLine($" - {ingredient.Value} {unit} of {ingredient.Key.Name}");
    }
}
