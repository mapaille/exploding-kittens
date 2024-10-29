using Mapaille.ExplodingKittens.WebApp.Extensions;

namespace Mapaille.ExplodingKittens.WebApp.Models;

public class GameModel
{
    public static event EventHandler? OnUpdate;

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public GameModel()
    {
        PlayerA = new(this);
        PlayerB = new(this);
    }

    public List<CardModel> Cards { get; set; } = [];
    public List<CardModel> DiscardedCards { get; } = [];

    public PlayerModel PlayerA { get; }
    public PlayerModel PlayerB { get; }

    public async Task ReinitializeAsync()
    {
        await SafeUpdateAsync(() =>
        {
            CollectCards();
            PassCards();
            return ValueTask.CompletedTask;
        });
    }

    private void PassCards()
    {
        var cards = new List<CardModel>();

        PlayerA.Cards.Add(new DefuseCardModel(this));
        PlayerB.Cards.Add(new DefuseCardModel(this));

        var cardsToPass = new List<CardModel>
        {
            new DefuseCardModel(this),
            new DefuseCardModel(this),
            new AttackCardModel(this),
            new AttackCardModel(this),
            new AttackCardModel(this),
            new AttackCardModel(this),
            new FavorCardModel(this),
            new FavorCardModel(this),
            new FavorCardModel(this),
            new FavorCardModel(this),
            new NopeCardModel(this),
            new NopeCardModel(this),
            new NopeCardModel(this),
            new NopeCardModel(this),
            new NopeCardModel(this),
            new ShuffleCardModel(this),
            new ShuffleCardModel(this),
            new ShuffleCardModel(this),
            new ShuffleCardModel(this),
            new SkipCardModel(this),
            new SkipCardModel(this),
            new SkipCardModel(this),
            new SkipCardModel(this),
            new DivinationCardModel(this),
            new DivinationCardModel(this),
            new DivinationCardModel(this),
            new DivinationCardModel(this),
            new DivinationCardModel(this),
            new CatCardModel(this, 1),
            new CatCardModel(this, 1),
            new CatCardModel(this, 1),
            new CatCardModel(this, 1),
            new CatCardModel(this, 2),
            new CatCardModel(this, 2),
            new CatCardModel(this, 2),
            new CatCardModel(this, 2),
            new CatCardModel(this, 3),
            new CatCardModel(this, 3),
            new CatCardModel(this, 3),
            new CatCardModel(this, 3),
            new CatCardModel(this, 4),
            new CatCardModel(this, 4),
            new CatCardModel(this, 4),
            new CatCardModel(this, 4),
            new CatCardModel(this, 5),
            new CatCardModel(this, 5),
            new CatCardModel(this, 5),
            new CatCardModel(this, 5)
        };

        cardsToPass = cardsToPass.Shuffle();

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

        cards.Add(new ExplodingKittenCardModel(this));

        cards = cards.Shuffle();

        foreach (var card in cards)
        {
            Cards.Add(card);
        }
    }

    private void CollectCards()
    {
        Cards.Clear();
        DiscardedCards.Clear();
        PlayerA.Cards.Clear();
        PlayerB.Cards.Clear();
    }

    public async Task SafeUpdateAsync(Func<ValueTask> func)
    {
        await _semaphore.WaitAsync();

        try
        {
            await func();
        }
        finally
        {
            OnUpdate?.Invoke(this, new EventArgs());
            _semaphore.Release();
        }
    }
}
