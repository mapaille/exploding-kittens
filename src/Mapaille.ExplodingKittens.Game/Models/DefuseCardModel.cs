namespace Mapaille.ExplodingKittens.Game.Models;

public record DefuseCardModel : CardModel
{
    public override CardType Type => CardType.Defuse;

    public override string Name => "Kit de désamorçage";
}
