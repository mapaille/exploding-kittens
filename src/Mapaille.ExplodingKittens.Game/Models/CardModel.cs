namespace Mapaille.ExplodingKittens.Game.Models;

public record CardModel
{
    public CardModel(CardType type)
    {
        Id = Guid.NewGuid();
        Type = type;
    }

    public Guid Id { get; }

    public CardType Type { get; }

    public string GetName()
    {
        return Type switch
        {
            CardType.ExplodingKitten => "Chaton explosif",
            CardType.Defuse => "Kit de désamorçage",
            CardType.Attack => "Attaque",
            CardType.Favor => "Faveur",
            CardType.Shuffle => "Mélange",
            CardType.Skip => "Passe-tour",
            CardType.Divination => "Divination",
            CardType.Nope => "Non !",
            CardType.Cat1 => "Chat 1",
            CardType.Cat2 => "Chat 2",
            CardType.Cat3 => "Chat 3",
            CardType.Cat4 => "Chat 4",
            CardType.Cat5 => "Chat 5",
            CardType.Peek => "Coup d'oeil",
            _ => string.Empty
        };
    }
}
