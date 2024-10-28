using Mapaille.ExplodingKittens.WebApp.Extensions;

namespace Mapaille.ExplodingKittens.WebApp.Models;

public record PlayerModel
{
    public readonly GameModel _gameModel;

    public PlayerModel(GameModel gameModel)
    {
        _gameModel = gameModel;
    }

    public List<CardModel> Cards { get; } = [];

    public Guid Id { get; } = Guid.NewGuid();

    public async void StealCard()
    {
        await _gameModel.SafeUpdateAsync(() =>
        {
            CardModel? card = null;

            if (_gameModel.PlayerA == this)
            {
                card = _gameModel.PlayerB.Cards.Shuffle().FirstOrDefault();

                if (card != null)
                {
                    _gameModel.PlayerB.Cards.Remove(card);
                }
            }
            else
            {
                card = _gameModel.PlayerA.Cards.Shuffle().FirstOrDefault();

                if (card != null)
                {
                    _gameModel.PlayerA.Cards.Remove(card);
                }
            }

            if (card != null)
            {
                Cards.Add(card);
            }

            return ValueTask.CompletedTask;
        });
    }

    public async void TakeCard()
    {
        await _gameModel.SafeUpdateAsync(() =>
        {
            var card = _gameModel.Cards.FirstOrDefault();

            if (card != null)
            {
                _gameModel.Cards.Remove(card);
                Cards.Add(card);
            }

            return ValueTask.CompletedTask;
        });
    }

    public bool CanTakeCard(CardModel card)
    {
        return _gameModel.Cards.FirstOrDefault() == card || _gameModel.DiscardedCards.Contains(card);
    }

    public async void TakeCard(CardModel card)
    {
        await _gameModel.SafeUpdateAsync(() =>
        {
            if ((_gameModel.Cards.FirstOrDefault() == card && _gameModel.Cards.Remove(card)) || _gameModel.DiscardedCards.Remove(card))
            {
                Cards.Add(card);
            }

            return ValueTask.CompletedTask;
        });
    }
}
