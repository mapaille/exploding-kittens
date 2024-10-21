namespace Mapaille.ExplodingKittens.WebApp.Models;

public record SkipCardModel : CardModel
{
    public SkipCardModel(GameModel game) : base(game)
    {
    }

    public override CardType Type => CardType.Skip;

    public override string Name => "Passe-tour";
}
