using Mapaille.ExplodingKittens.WebApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Game : IAsyncDisposable
{
    [Inject]
    [NotNull]
    public GameModel? Model { get; set; }

    [Inject]
    [NotNull]
    public IJSRuntime? JS { get; set; }

    public PlayerModel? Player { get; set; }

    public async Task SeeTheFuture()
    {
        var next3CardNames = Model.Cards.Take(3).Select(x => x.Name);
        var message = string.Join(", ", next3CardNames);
        await JS.InvokeVoidAsync("showAlert", message);
    }

    public string GetActivePlayerClass(PlayerModel? player, PlayerModel target)
    {
        if (player == target)
        {
            return "active";
        }

        return string.Empty;
    }

    protected override Task OnInitializedAsync()
    {
        Model.OnUpdate += UpdateState;
        return base.OnInitializedAsync();
    }

    private async void UpdateState(object? sender, EventArgs args)
    {
        await InvokeAsync(StateHasChanged);
    }

    public void SelectPlayerA()
    {
        if (IsPlayerA())
        {
            Player = null;
        }
        else
        {
            Player = Model.PlayerA;
        }

        StateHasChanged();
    }

    public void SelectPlayerB()
    {
        if (IsPlayerB())
        {
            Player = null;
        }
        else
        {
            Player = Model.PlayerB;
        }

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
        Model.OnUpdate -= UpdateState;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
