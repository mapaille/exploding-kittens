namespace Mapaille.ExplodingKittens.Game.Models;

public class GameModel(PlayerModel playerA, PlayerModel playerB)
{
    public event EventHandler? OnUpdate;

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public List<CardModel> Cards { get; } = [];
    public List<CardModel> DiscardedCards { get; } = [];

    public PlayerModel PlayerA { get; } = playerA;
    public PlayerModel PlayerB { get; } = playerB;

    public bool PlayerExploded => PlayerA.IsExploded || PlayerB.IsExploded;

    public async Task ResetAsync()
    {
        await SafeUpdateAsync(() =>
        {
            ClearCards();
            PassCards();
        });
    }

    public async Task SafeUpdateAsync(Action action)
    {
        await _semaphore.WaitAsync();

        try
        {
            action();
        }
        finally
        {
            OnUpdate?.Invoke(this, EventArgs.Empty);
            _semaphore.Release();
        }
    }

    private void PassCards()
    {
        var cards = new List<CardModel>();

        PlayerA.Cards.Add(CardType.Defuse);
        PlayerB.Cards.Add(CardType.Defuse);

        var cardsToPass = new List<CardModel>
        {
            { CardType.Defuse, 2 },
            { CardType.Attack, 4 },
            { CardType.Favor, 4 },
            { CardType.Nope, 5 },
            { CardType.Shuffle, 4 },
            { CardType.Skip, 4 },
            { CardType.Divination, 5 },
            { CardType.Cat1, 4 },
            { CardType.Cat2, 4 },
            { CardType.Cat3, 4 },
            { CardType.Cat4, 4 },
            { CardType.Cat5, 4 }
        };

        cardsToPass.Shuffle();

        for (var i = 0; i < cardsToPass.Count; i++)
        {
            if (i %  2 == 0)
            {
                if (PlayerA.Cards.Count < 8)
                {
                    PlayerA.Cards.Add(cardsToPass.ElementAt(i));
                    continue;
                }
            }
            else
            {
                if (PlayerB.Cards.Count < 8)
                {
                    PlayerB.Cards.Add(cardsToPass.ElementAt(i));
                    continue;
                }
            }

            cards.Add(cardsToPass.ElementAt(i));
        }

        cards.Add(CardType.ExplodingKitten);
        cards.Shuffle();
        Cards.AddRange(cards);
    }

    private void ClearCards()
    {
        Cards.Clear();
        DiscardedCards.Clear();
        PlayerA.Cards.Clear();
        PlayerB.Cards.Clear();
    }
}
