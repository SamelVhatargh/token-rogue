namespace Battle.CombatActions
{
    public class SkipAction : ICombatAction
    {
        private readonly string _name;

        public SkipAction(string name)
        {
            _name = name;
        }
        
        public void Execute()
        {
        }

        public string GetLogMessage()
        {
            return $"{_name} has nothing left to do";
        }
    }
}