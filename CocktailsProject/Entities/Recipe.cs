using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CocktailsProject.Entities;

public class Recipe
{
    public int Id { get; set; }
    public string IngredientsJson { get; set; }

    private Dictionary<Ingredient, double> _ingredients;

    [NotMapped]
    public Dictionary<Ingredient, double> Ingredients
    {
        get
        {
            if (_ingredients == null)
            {
                _ingredients = JsonConvert.DeserializeObject<Dictionary<Ingredient, double>>(IngredientsJson)!;
            }

            return _ingredients;
        }
        set
        {
            _ingredients = value;
            IngredientsJson = JsonConvert.SerializeObject(value);
        }
    }
}