using System.ComponentModel.DataAnnotations;

namespace CocktailsProject.Entities;

public class Drink
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public Recipe Recipe { get; set; }
}