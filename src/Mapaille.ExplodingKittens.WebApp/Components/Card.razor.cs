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
            CardType.Cat => $"cat-{((CatCardModel)Model).Number}",
            _ => string.Empty,
        };
    }
}
