namespace Mapaille.ExplodingKittens.WebApp;

public class StaticFilesVersion
{
    public string Value { get; } = Guid.NewGuid().ToString().Replace("-", "");
}
