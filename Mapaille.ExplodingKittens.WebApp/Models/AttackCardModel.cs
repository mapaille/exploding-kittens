namespace Mapaille.ExplodingKittens.WebApp.Models;

public record AttackCardModel : CardModel
{
    public override CardType Type => CardType.Attack;

    public override string Name => "Attaque";
}
