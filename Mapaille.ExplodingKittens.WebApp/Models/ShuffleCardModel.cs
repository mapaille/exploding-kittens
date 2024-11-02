namespace Mapaille.ExplodingKittens.WebApp.Models;

public record ShuffleCardModel : CardModel
{
    public override CardType Type => CardType.Shuffle;

    public override string Name => "Mélange";
}
