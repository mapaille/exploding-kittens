namespace Mapaille.ExplodingKittens.WebApp.Models;

public record FavorCardModel : CardModel
{
    public FavorCardModel(GameModel game) : base(game)
    {
    }

    public override CardType Type => CardType.Favor;

    public override string Name => "Faveur";
}
