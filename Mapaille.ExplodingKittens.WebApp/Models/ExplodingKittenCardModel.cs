namespace Mapaille.ExplodingKittens.WebApp.Models;

public record ExplodingKittenCardModel(GameModel game) : CardModel(game)
{
    public override CardType Type => CardType.ExplodingKitten;

    public override string Name => "Chaton explosif";
}
