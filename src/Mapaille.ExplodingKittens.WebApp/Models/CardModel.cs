namespace Mapaille.ExplodingKittens.WebApp.Models;

public abstract record CardModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public abstract CardType Type { get; }

    public abstract string Name { get; }
}
