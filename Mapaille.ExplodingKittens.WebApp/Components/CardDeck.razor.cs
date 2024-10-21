using Mapaille.ExplodingKittens.WebApp.Models;
using Microsoft.AspNetCore.Components;

namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class CardDeck
{
    [Parameter]
    [EditorRequired]
    public required List<CardModel> Cards { get; set; }
}
