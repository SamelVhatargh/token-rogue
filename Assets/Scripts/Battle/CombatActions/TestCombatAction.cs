using UnityEngine;

namespace Battle.CombatActions
{
    public class TestCombatAction : ICombatAction
    {
        public void Execute()
        {
            Debug.Log("Test combat action performed");
        }
    }
}