using Microsoft.AspNetCore.Components;

namespace Mapaille.ExplodingKittens.WebApp.Components;

public partial class Home
{
    [SupplyParameterFromForm]
    public string? SecretPhrase { get; set; }

    public bool IsAuthenticated { get; set; }

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
}
