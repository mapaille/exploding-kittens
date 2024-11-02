namespace Mapaille.ExplodingKittens.WebApp.Models;

public record ExplodingKittenCardModel : CardModel
{
    public override CardType Type => CardType.ExplodingKitten;

    public override string Name => "Chaton explosif";
}
