using Mapaille.ExplodingKittens.Game.Models;
using Microsoft.AspNetCore.Components;

namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Player
{
    [Parameter]
    [EditorRequired]
    public required PlayerModel Model { get; set; }
}
