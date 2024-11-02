namespace Mapaille.ExplodingKittens.Game.Extensions;

public static class ListExtensions
{
    public static void Add(this List<CardModel> source, CardType type)
    {
        source.Add(new CardModel(type));
    }

    public static void Add(this List<CardModel> source, CardType type, int count)
    {
        for (var i = 0; i < count; i++)
        {
            Add(source, type);
        }
    }

    public static List<CardModel> Shuffle(this List<CardModel> source)
    {
        var random = new Random();
        return [.. source.OrderBy(_ => random.Next())];
    }

    public static void InsertRandomly<CardModel>(this List<CardModel> source, CardModel item)
    {
        var random = new Random();
        int randomIndex = random.Next(0, source.Count + 1);
        source.Insert(randomIndex, item);
    }
}
