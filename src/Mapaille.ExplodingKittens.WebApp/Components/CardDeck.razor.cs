namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class CardDeck
{
    [Parameter]
    [EditorRequired]
    public required List<CardModel> Cards { get; set; }

    [Parameter]
    public required PlayerModel? ActivePlayer { get; set; }
}
