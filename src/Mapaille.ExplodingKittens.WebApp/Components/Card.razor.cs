namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Card
{
    [Parameter]
    [EditorRequired]
    public required CardModel Model { get; set; }

    [Parameter]
    public required PlayerModel? ActivePlayer { get; set; }

    [Parameter]
    [EditorRequired]
    public required ButtonsPosition ButtonsPosition { get; set; }

    [Inject]
    [NotNull]
    public GameModel? Game { get; set; }

    private string GetButtonsPositionClass()
    {
        return ButtonsPosition switch
        {
            ButtonsPosition.Bottom => "flex-bottom",
            ButtonsPosition.Top => "flex-top",
            _ => string.Empty,
        };
    }

    private async Task DiscardCardAsync()
    {
        if (ActivePlayer != null)
        {
            await ActivePlayer.DiscardCard(Model);
        }
    }

    private async Task GiveCardAsync()
    {
        if (ActivePlayer != null)
        {
            await ActivePlayer.GiveCard(Model);
        }
    }

    private async Task PutCardBackInPileAsync()
    {
        if (ActivePlayer != null)
        {
            await ActivePlayer.PutCardBackInPile(Model);
        }
    }

    private async Task PickCardAsync()
    {
        if (ActivePlayer != null)
        {
            await ActivePlayer.PickCard(Model);
        }
    }

    private static string CardTypeClass(CardType cardType)
    {
        return cardType switch
        {
            CardType.ExplodingKitten => "exploding-kitten",
            CardType.Attack => "attack",
            CardType.Cat1 => "cat-1",
            CardType.Cat2 => "cat-2",
            CardType.Cat3 => "cat-3",
            CardType.Cat4 => "cat-4",
            CardType.Cat5 => "cat-5",
            CardType.Divination => "divination",
            CardType.Defuse => "defuse",
            CardType.Favor => "favor",
            CardType.Nope => "nope",
            CardType.Shuffle => "shuffle",
            CardType.Skip => "skip",
            CardType.Peek => "peek",
            _ => string.Empty,
        };
    }
}
