namespace Mapaille.ExplodingKittens.Game.Extensions;

public static class ListExtensions
{
    private static readonly Random _random = new();

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

    public static void Shuffle(this List<CardModel> source)
    {
        int n = source.Count;
        while (n > 1)
        {
            n--;
            int k = _random.Next(n + 1);
            (source[n], source[k]) = (source[k], source[n]);
        }
    }

    public static CardModel? GetRandomly(this List<CardModel> source)
    {
        if (source.Count == 0)
        {
            return null;
        }

        int index = _random.Next(source.Count);
        return source[index];
    }

    public static void InsertRandomly<CardModel>(this List<CardModel> source, CardModel item)
    {
        int randomIndex = _random.Next(source.Count + 1);
        source.Insert(randomIndex, item);
    }
}
