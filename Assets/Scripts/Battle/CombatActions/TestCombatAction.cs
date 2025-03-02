using UnityEngine;

namespace Battle.CombatActions
{
    public class TestCombatAction : ICombatAction
    {
        public void Execute()
        {
        }

        public string GetLogMessage()
        {
            return "Test combat action performed";
        }
    }
}