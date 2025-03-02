namespace Battle.CombatActions
{
    public interface ICombatAction
    {
        public void Execute();

        public string GetLogMessage();
    }
}