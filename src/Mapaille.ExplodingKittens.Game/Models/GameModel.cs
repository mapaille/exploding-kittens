﻿namespace Mapaille.ExplodingKittens.Game.Models;

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

    public async Task ShuffleCardsAsync()
    {
        await SafeUpdateAsync(() => Cards.Shuffle());
    }

    private void PassCards()
    {
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

        do
        {
            if (PlayerA.Cards.Count < 8)
            {
                var card = cardsToPass.First();
                cardsToPass.Remove(card);
                PlayerA.Cards.Add(card);
            }

            if (PlayerB.Cards.Count < 8)
            {
                var card = cardsToPass.First();
                cardsToPass.Remove(card);
                PlayerB.Cards.Add(card);
            }
        }
        while (PlayerA.Cards.Count != 8 && PlayerB.Cards.Count != 8);

        Cards.AddRange(cardsToPass);
        Cards.Add(CardType.ExplodingKitten);
        Cards.Add(CardType.Peek, 2);
        Cards.Shuffle();
    }

    private void ClearCards()
    {
        Cards.Clear();
        DiscardedCards.Clear();
        PlayerA.Cards.Clear();
        PlayerB.Cards.Clear();
    }
}
