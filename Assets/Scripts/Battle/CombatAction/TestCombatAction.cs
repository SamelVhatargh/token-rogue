using UnityEngine;

namespace Battle.CombatAction
{
    public class TestCombatAction : ICombatAction
    {
        public void Execute()
        {
            Debug.Log("Test combat action performed");
        }
    }
}