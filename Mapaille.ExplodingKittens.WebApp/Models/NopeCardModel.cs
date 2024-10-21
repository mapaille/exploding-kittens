namespace Mapaille.ExplodingKittens.WebApp.Models;

public record NopeCardModel : CardModel
{
    public NopeCardModel(GameModel game) : base(game)
    {
    }

    public override CardType Type => CardType.Nope;

    public override string Name => "Non !";
}
