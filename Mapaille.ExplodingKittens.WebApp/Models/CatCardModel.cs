namespace Mapaille.ExplodingKittens.WebApp.Models;

public record CatCardModel : CardModel
{
    private readonly int _number;

    public CatCardModel(GameModel game, int number) : base(game)
    {
        _number = number;
    }

    public override CardType Type => CardType.Cat;

    public override string Name => $"Chat {_number}";

    public int Number => _number;
}
