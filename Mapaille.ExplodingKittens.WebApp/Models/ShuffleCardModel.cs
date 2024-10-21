namespace Mapaille.ExplodingKittens.WebApp.Models;

public record ShuffleCardModel : CardModel
{
    public ShuffleCardModel(GameModel game) : base(game)
    {
    }

    public override CardType Type => CardType.Shuffle;

    public override string Name => "Mélange";
}
