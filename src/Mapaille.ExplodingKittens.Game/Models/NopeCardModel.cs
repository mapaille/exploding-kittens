namespace Mapaille.ExplodingKittens.Game.Models;

public record NopeCardModel : CardModel
{
    public override CardType Type => CardType.Nope;

    public override string Name => "Non !";
}
