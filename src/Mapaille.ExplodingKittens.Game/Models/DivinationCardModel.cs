namespace Mapaille.ExplodingKittens.Game.Models;

public record DivinationCardModel : CardModel
{
    public override CardType Type => CardType.Divination;

    public override string Name => "Divination";
}
