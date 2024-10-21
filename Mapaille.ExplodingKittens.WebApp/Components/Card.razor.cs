using Mapaille.ExplodingKittens.WebApp.Models;
using Microsoft.AspNetCore.Components;

namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Card
{
    [Parameter]
    [EditorRequired]
    public required CardModel Model { get; set; }

    [Parameter]
    [EditorRequired]
    public required PlayerModel PlayerModel { get; set; }
}
