namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Game : IAsyncDisposable
{
    [Inject]
    public GameModel Model { get; set; } = null!;

    [Inject]
    public IJSRuntime JS { get; set; } = null!;

    public PlayerModel? Player { get; set; }

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

    public bool IsPlayerExploded()
    {
        return Model.PlayerExploded;
    }

    public async Task ResetGame()
    {
        await Model.Reset();
    }

    public async Task SeeTheFuture()
    {
        if (Player != null)
        {
            var future = Player.SeeTheFuture();
            await JS.InvokeVoidAsync("showAlert", future);
        }
    }

    public async Task ShuffleGameCards()
    {
        if (Player != null)
        {
            await Player.ShuffleGameCards();
            await JS.InvokeVoidAsync("showAlert", "Cartes mélangées");
        }
    }

    public async Task PickGameCard()
    {
        if (Player != null)
        {
            await Player.PickGameCard();
        }
    }

    public async Task StealPlayerCard()
    {
        if (Player != null)
        {
            await Player.StealPlayerCard();
        }
    }

    public ValueTask DisposeAsync()
    {
        Model.OnUpdate -= UpdateState;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
