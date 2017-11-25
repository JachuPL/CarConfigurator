namespace CarConfigurator.Core.Abstractions.ActionPossibility
{
    public sealed class ActionPossible : IActionPossible
    {
        public bool IsPossible => true;

        public string Reason => string.Empty;
    }
}