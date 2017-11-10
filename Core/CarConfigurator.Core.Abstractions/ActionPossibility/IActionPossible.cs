namespace CarConfigurator.Core.Abstractions.ActionPossibility
{
    public interface IActionPossible
    {
        bool IsPossible { get; }
        string Reason { get; }
    }
}