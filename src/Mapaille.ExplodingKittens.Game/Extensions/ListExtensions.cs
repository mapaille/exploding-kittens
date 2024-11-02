using Mapaille.ExplodingKittens.Game.Models;

namespace Mapaille.ExplodingKittens.Game.Extensions;

public static class ListExtensions
{
    public static List<CardModel> Shuffle(this List<CardModel> source)
    {
        var random = new Random();
        return [.. source.OrderBy(_ => random.Next())];
    }

    public static void InsertRandomly<CardModel>(this List<CardModel> list, CardModel item)
    {
        var random = new Random();
        int randomIndex = random.Next(0, list.Count + 1);
        list.Insert(randomIndex, item);
    }
}
