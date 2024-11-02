using Mapaille.ExplodingKittens.Game.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Card
{
    [Parameter]
    [EditorRequired]
    public required CardModel Model { get; set; }

    [Parameter]
    public required PlayerModel? ActivePlayer { get; set; }

    [Inject]
    [NotNull]
    public GameModel? Game { get; set; }

    public string CardTypeClass(CardType cardType)
    {
        return cardType switch
        {
            CardType.ExplodingKitten => "exploding-kitten",
            CardType.Cat1 => $"cat-1",
            CardType.Cat2 => $"cat-2",
            CardType.Cat3 => $"cat-3",
            CardType.Cat4 => $"cat-4",
            CardType.Cat5 => $"cat-5",
            _ => string.Empty,
        };
    }
}
