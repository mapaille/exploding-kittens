namespace Mapaille.ExplodingKittens.Game.Models;

public record ShuffleCardModel : CardModel
{
    public override CardType Type => CardType.Shuffle;

    public override string Name => "Mélange";
}
