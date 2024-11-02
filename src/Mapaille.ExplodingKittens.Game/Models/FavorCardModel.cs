namespace Mapaille.ExplodingKittens.Game.Models;

public record FavorCardModel : CardModel
{
    public override CardType Type => CardType.Favor;

    public override string Name => "Faveur";
}
