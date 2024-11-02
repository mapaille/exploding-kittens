namespace Mapaille.ExplodingKittens.Game.Models;

public record SkipCardModel : CardModel
{
    public override CardType Type => CardType.Skip;

    public override string Name => "Passe-tour";
}
