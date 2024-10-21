namespace Mapaille.ExplodingKittens.WebApp.Services;

public class VersionService
{
    public VersionService()
    {
        Version = Guid.NewGuid().ToString().Replace("-", "");
    }

    public string Version { get; }
}
