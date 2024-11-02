namespace Mapaille.ExplodingKittens.Game.Models;

public record AttackCardModel : CardModel
{
    public override CardType Type => CardType.Attack;

    public override string Name => "Attaque";
}
