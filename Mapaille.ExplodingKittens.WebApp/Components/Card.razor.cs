using Mapaille.ExplodingKittens.WebApp.Models;
using Microsoft.AspNetCore.Components;

namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Card
{
    [Parameter]
    [EditorRequired]
    public required CardModel Model { get; set; }

    public string CardTypeClass(CardType cardType)
    {
        return cardType switch
        {
            CardType.ExplodingKitten => "exploding-kitten",
            _ => string.Empty,
        };
    }
}
