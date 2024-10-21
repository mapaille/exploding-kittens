namespace Mapaille.ExplodingKittens.WebApp.Models;

public record DefuseCardModel(GameModel game) : CardModel(game)
{
    public override CardType Type => CardType.Defuse;

    public override string Name => "Kit de désamorçage";
}
