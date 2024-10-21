namespace Mapaille.ExplodingKittens.WebApp.Models;

public record AttackCardModel : CardModel
{
    public AttackCardModel(GameModel game) : base(game)
    {
    }

    public override CardType Type => CardType.Attack;

    public override string Name => "Attaque";
}
