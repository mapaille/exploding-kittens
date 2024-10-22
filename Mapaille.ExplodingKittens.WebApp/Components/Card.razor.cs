using Mapaille.ExplodingKittens.WebApp.Models;
using Microsoft.AspNetCore.Components;

namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Card
{
    [Parameter]
    [EditorRequired]
    public required CardModel Model { get; set; }

    public string BombClass(CardType cardType)
    {
        if (cardType == CardType.ExplodingKitten)
        {
            return "bomb";
        }

        return string.Empty;
    }
}
