namespace Mapaille.ExplodingKittens.Game.Models;

public class GameModel(PlayerModel playerA, PlayerModel playerB)
{
    public event EventHandler? OnUpdate;

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public List<CardModel> Cards { get; set; } = [];
    public List<CardModel> DiscardedCards { get; } = [];

    public PlayerModel PlayerA { get; } = playerA;
    public PlayerModel PlayerB { get; } = playerB;

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
        catch
        {
            //TODO Restore previous state.
        }
        finally
        {
            OnUpdate?.Invoke(this, new EventArgs());
            //Save state.
            _semaphore.Release();
        }
    }

    private void PassCards()
    {
        var cards = new List<CardModel>();

        PlayerA.Cards.Add(new DefuseCardModel());
        PlayerB.Cards.Add(new DefuseCardModel());

        var cardsToPass = new List<CardModel>();

        cardsToPass.AddRange(CreateManyCards<DefuseCardModel>(2));
        cardsToPass.AddRange(CreateManyCards<AttackCardModel>(4));
        cardsToPass.AddRange(CreateManyCards<FavorCardModel>(4));
        cardsToPass.AddRange(CreateManyCards<NopeCardModel>(5));
        cardsToPass.AddRange(CreateManyCards<ShuffleCardModel>(4));
        cardsToPass.AddRange(CreateManyCards<SkipCardModel>(4));
        cardsToPass.AddRange(CreateManyCards<DivinationCardModel>(5));
        cardsToPass.AddRange(CreateManyCatCards(4, 1));
        cardsToPass.AddRange(CreateManyCatCards(4, 2));
        cardsToPass.AddRange(CreateManyCatCards(4, 3));
        cardsToPass.AddRange(CreateManyCatCards(4, 4));
        cardsToPass.AddRange(CreateManyCatCards(4, 4));

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

        cards.Add(new ExplodingKittenCardModel());

        cards = cards.Shuffle();

        foreach (var card in cards)
        {
            Cards.Add(card);
        }
    }

    private void ClearCards()
    {
        Cards.Clear();
        DiscardedCards.Clear();
        PlayerA.Cards.Clear();
        PlayerB.Cards.Clear();
    }

    private static IEnumerable<TCardModel> CreateManyCards<TCardModel>(int count)
        where TCardModel : CardModel, new()
    {
        for (var i = 0; i < count; i++)
        {
            yield return new TCardModel();
        }
    }

    private static IEnumerable<CatCardModel> CreateManyCatCards(int count, int number)
    {
        for (var i = 0; i < count; i++)
        {
            yield return new CatCardModel(number);
        }
    }
}
