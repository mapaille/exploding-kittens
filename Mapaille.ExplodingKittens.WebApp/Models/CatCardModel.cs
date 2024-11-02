namespace Mapaille.ExplodingKittens.WebApp.Models;

public record CatCardModel : CardModel
{
    private readonly int _number;

    public CatCardModel(int number)
    {
        _number = number;
    }

    public override CardType Type => CardType.Cat;

    public override string Name => $"Chat {_number}";

    public int Number => _number;
}
