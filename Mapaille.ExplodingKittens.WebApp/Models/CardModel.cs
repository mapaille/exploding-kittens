using Mapaille.ExplodingKittens.WebApp.Extensions;
using Microsoft.AspNetCore.Components;

namespace Mapaille.ExplodingKittens.WebApp.Models;

public abstract record CardModel
{
    private readonly GameModel _game;

    protected CardModel(GameModel game)
    {
        _game = game;
    }

    public Guid Id { get; } = Guid.NewGuid();

    public abstract CardType Type { get; }

    public abstract string Name { get; }

    public bool CanBeDiscarded()
    {
        return IsInPlayerHands();
    }

    public bool CanBeTransferred()
    {
        return IsInPlayerHands();
    }

    public bool CanBePutBackInPile()
    {
        return IsInPlayerHands() && Type == CardType.ExplodingKitten;
    }

    public bool CanBeTaken()
    {
        return IsDiscarded();
    }

    public EventCallback TakeCard(PlayerModel player)
    {
        _game.SafeUpdateAsync(() =>
        {
            var removedFromDiscardedCards = _game.DiscardedCards.Remove(this);

            if (removedFromDiscardedCards)
            {
                player.Cards.Add(this);
            }

            return ValueTask.CompletedTask;
        }).GetAwaiter().GetResult();

        return new EventCallback();
    }

    public async void PutBackInPile()
    {
        await _game.SafeUpdateAsync(() =>
        {
            var removedFromPlayerA = _game.PlayerA.Cards.Remove(this);

            if (removedFromPlayerA)
            {
                _game.Cards.InsertRandomly(this);
                return ValueTask.CompletedTask;

            }

            var removedFromPlayerB = _game.PlayerB.Cards.Remove(this);

            if (removedFromPlayerB)
            {
                _game.Cards.InsertRandomly(this);
            }

            return ValueTask.CompletedTask;
        });
    }

    public async void Transfer()
    {
        await _game.SafeUpdateAsync(() =>
        {
            var removedFromPlayerA = _game.PlayerA.Cards.Remove(this);

            if (removedFromPlayerA)
            {
                _game.PlayerB.Cards.Add(this);
                return ValueTask.CompletedTask;

            }

            var removedFromPlayerB = _game.PlayerB.Cards.Remove(this);

            if (removedFromPlayerB)
            {
                _game.PlayerA.Cards.Add(this);
            }

            return ValueTask.CompletedTask;

        });
    }

    public async void Discard()
    {
        await _game.SafeUpdateAsync(() =>
        {
            _game.PlayerA.Cards.Remove(this);
            _game.PlayerB.Cards.Remove(this);
            _game.DiscardedCards.Add(this);

            if (Type == CardType.Shuffle)
            {
                _game.Cards.Shuffle();
            }

            return ValueTask.CompletedTask;
        });
    }

    private bool IsInPlayerHands()
    {
        if (_game.PlayerA.Cards.Contains(this))
        {
            return true;
        }

        if (_game.PlayerB.Cards.Contains(this))
        {
            return true;
        }

        return false;
    }

    private bool IsDiscarded()
    {
        return _game.DiscardedCards.Contains(this);
    }
}
