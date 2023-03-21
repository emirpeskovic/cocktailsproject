using CocktailsProject.Entities;
using Newtonsoft.Json;

namespace CocktailsProject;

public static class DictionaryExtensions
{
    public static string ToJson(this Dictionary<Ingredient, double> dictionary)
    {
        var list = dictionary.Select(pair => new { identifier = pair.Key.Id, amount = pair.Value }).ToList();
        return JsonConvert.SerializeObject(list);
    }

    public static Dictionary<Ingredient, double> FromJson(string json)
    {
        var list = JsonConvert.DeserializeObject<List<dynamic>>(json)!;

        var dictionary = new Dictionary<Ingredient, double>();

        foreach (var item in list)
        {
            var ingredient = new Ingredient { Id = item.identifier };
            dictionary.Add(ingredient, (double)item.amount);
        }

        return dictionary;
    }
}