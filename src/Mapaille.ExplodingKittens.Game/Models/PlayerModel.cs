namespace Mapaille.ExplodingKittens.Game.Models;

public class PlayerModel
{
    public Guid Id { get; } = Guid.NewGuid();

    public List<CardModel> Cards { get; } = [];

    public bool IsExploded => Cards.Any(x => x.Type == CardType.ExplodingKitten);

    public async void PutCardBackInPile(GameModel game, CardModel card)
    {
        await game.SafeUpdateAsync(() =>
        {
            if (Cards.Remove(card))
            {
                game.Cards.InsertRandomly(card);
            }
        });
    }

    public async void StealCard(GameModel game)
    {
        await game.SafeUpdateAsync(() =>
        {
            CardModel? card = null;

            if (game.PlayerA == this)
            {
                card = game.PlayerB.Cards.GetRandomly();

                if (card != null)
                {
                    game.PlayerB.Cards.Remove(card);
                }
            }
            else
            {
                card = game.PlayerA.Cards.GetRandomly();

                if (card != null)
                {
                    game.PlayerA.Cards.Remove(card);
                }
            }

            if (card != null)
            {
                Cards.Add(card);
            }
        });
    }

    public async void PickCard(GameModel game)
    {
        await game.SafeUpdateAsync(() =>
        {
            var card = game.Cards.FirstOrDefault();

            if (card != null)
            {
                game.Cards.Remove(card);
                Cards.Add(card);
            }
        });
    }

    public async void PickCard(GameModel game, CardModel card)
    {
        await game.SafeUpdateAsync(() =>
        {
            if (game.Cards.Remove(card) || game.DiscardedCards.Remove(card))
            {
                Cards.Add(card);
            }
        });
    }

    public bool CanDiscardCard(CardModel card)
    {
        return card.Type != CardType.ExplodingKitten && Cards.Contains(card);
    }

    public bool CanGiveCard(CardModel card)
    {
        return card.Type != CardType.ExplodingKitten && Cards.Contains(card);
    }

    public bool CanPutCardBackInPile(CardModel card)
    {
        return card.Type == CardType.ExplodingKitten && Cards.Contains(card);
    }

    public static bool CanPickCard(GameModel game, CardModel card)
    {
        return game.Cards.FirstOrDefault() == card || game.DiscardedCards.Contains(card);
    }

    public async void GiveCard(GameModel game, CardModel card)
    {
        await game.SafeUpdateAsync(() =>
        {
            if (!Cards.Remove(card)) return;
            
            if (game.PlayerA != this)
            {
                game.PlayerA.Cards.Add(card);
            }
            else
            {
                game.PlayerB.Cards.Add(card);
            }
        });
    }

    public async void DiscardCard(GameModel game, CardModel card)
    {
        await game.SafeUpdateAsync(() =>
        {
            if (!Cards.Remove(card)) return;
            game.DiscardedCards.Add(card);
        });
    }
}
