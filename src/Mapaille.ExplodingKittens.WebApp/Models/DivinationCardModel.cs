namespace Mapaille.ExplodingKittens.WebApp.Models;

public record DivinationCardModel : CardModel
{
    public override CardType Type => CardType.Divination;

    public override string Name => "Divination";
}
