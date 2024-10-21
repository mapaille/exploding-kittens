using Mapaille.ExplodingKittens.WebApp.Models;
using Microsoft.AspNetCore.Components.Web;

namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Game : IAsyncDisposable
{
    public static GameModel Model { get; } = new();
    public PlayerModel? Player { get; set; }

    public Game()
    {
        GameModel.OnUpdate += UpdateState;
    }

    public string? SecretPhrase { get; set; }

    public bool IsAuthenticated { get; set; }

    public void HandleKeyPress(KeyboardEventArgs args)
    {
        if (args.Code == "Enter")
        {
            Authenticate();
        }
    }

    public void Authenticate()
    {
        if (SecretPhrase?.Equals("marc-estelle", StringComparison.InvariantCultureIgnoreCase) == true)
        {
            IsAuthenticated = true;
        }
        else
        {
            IsAuthenticated = false;
            SecretPhrase = null;
        }

        StateHasChanged();
    }

    private async void UpdateState(object? sender, EventArgs args)
    {
        await InvokeAsync(StateHasChanged);
    }

    public void SelectPlayerA()
    {
        Player = Model.PlayerA;
        StateHasChanged();
    }

    public void SelectPlayerB()
    {
        Player = Model.PlayerB;
        StateHasChanged();
    }

    public bool IsPlayerA()
    {
        return Player == Model.PlayerA;
    }

    public bool IsPlayerB()
    {
        return Player == Model.PlayerB;
    }

    public ValueTask DisposeAsync()
    {
        GameModel.OnUpdate -= UpdateState;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
