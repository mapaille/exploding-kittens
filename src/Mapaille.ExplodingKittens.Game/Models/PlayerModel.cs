namespace Mapaille.ExplodingKittens.Game.Models;

public class PlayerModel(GameModel game)
{
    public Guid Id { get; } = Guid.NewGuid();

    public List<CardModel> Cards { get; } = [];

    public bool IsExploded => Cards.Any(x => x.Type == CardType.ExplodingKitten);

    public Task PutCardBackInPile(CardModel card)
    {
        return game.SynchronizeUpdateAsync(() =>
        {
            if (Cards.Remove(card))
            {
                game.Cards.InsertRandomly(card);
            }
        });
    }

    public Task StealCard()
    {
        return game.SynchronizeUpdateAsync(() =>
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

    public Task PickCard()
    {
        return game.SynchronizeUpdateAsync(() =>
        {
            var card = game.Cards.FirstOrDefault();

            if (card != null)
            {
                game.Cards.Remove(card);
                Cards.Add(card);
            }
        });
    }

    public Task PickCard(CardModel card)
    {
        return game.SynchronizeUpdateAsync(() =>
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

    public bool CanPickCard(CardModel card)
    {
        return game.Cards.FirstOrDefault() == card || game.DiscardedCards.Contains(card);
    }

    public Task GiveCard(CardModel card)
    {
        return game.SynchronizeUpdateAsync(() =>
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

    public Task DiscardCard(CardModel card)
    {
        return game.SynchronizeUpdateAsync(() =>
        {
            if (!Cards.Remove(card)) return;
            game.DiscardedCards.Add(card);
        });
    }
}
