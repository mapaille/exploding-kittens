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

    private bool IsDiscarded => _game.DiscardedCards.Contains(this);

    private bool IsInPlayerHands => IsInPlayerAHands || IsInPlayerBHands;

    private bool IsInPlayerAHands => _game.PlayerA.Cards.Contains(this);

    private bool IsInPlayerBHands => _game.PlayerB.Cards.Contains(this);

    public Guid Id { get; } = Guid.NewGuid();

    public abstract CardType Type { get; }

    public abstract string Name { get; }

    public bool CanBeDiscarded()
    {
        return IsInPlayerHands;
    }

    public bool CanBeTransferred()
    {
        return IsInPlayerHands;
    }

    public bool CanBePutBackInPile()
    {
        return IsInPlayerHands && Type == CardType.ExplodingKitten;
    }

    public bool CanBeGiven()
    {
        return IsDiscarded;
    }

    public async void GiveToPlayer(PlayerModel player)
    {
        await _game.SafeUpdateAsync(() =>
        {
            var removedFromDiscardedCards = _game.DiscardedCards.Remove(this);

            if (removedFromDiscardedCards)
            {
                player.Cards.Add(this);
            }

            return ValueTask.CompletedTask;
        });
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
}
