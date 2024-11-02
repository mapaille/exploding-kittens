namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Player
{
    [Parameter]
    [EditorRequired]
    public required PlayerModel Model { get; set; }
}
