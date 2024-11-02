namespace Mapaille.ExplodingKittens.WebApp.Models;

public record FavorCardModel : CardModel
{
    public override CardType Type => CardType.Favor;

    public override string Name => "Faveur";
}
