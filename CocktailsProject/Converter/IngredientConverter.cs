using CocktailsProject.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace CocktailsProject.Converter;

public class IngredientConverter : ValueConverter<Dictionary<Ingredient, double>, string>
{
    public IngredientConverter() : base(
        ingredients => JsonConvert.SerializeObject(ingredients),
        json => JsonConvert.DeserializeObject<Dictionary<Ingredient, double>>(json),
        new ConverterMappingHints(size: 100))
    { }
}