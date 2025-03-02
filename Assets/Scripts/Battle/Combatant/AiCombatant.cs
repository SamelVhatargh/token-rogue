using System;
using System.Collections;
using Battle.CombatAction;
using UnityEngine;

namespace Battle.Combatant
{
    public class AiCombatant : MonoBehaviour, ICombatant
    {
        public event Action<ICombatAction> OnActionTaken;
        
        public void DoCombatAction()
        {
            StartCoroutine(DelayedAction());
        }
        
        private IEnumerator DelayedAction()
        {
            yield return new WaitForSeconds(1f);
            OnActionTaken?.Invoke(new TestCombatAction());
        }

        public override string ToString()
        {
            return "AiCombatant";
        }
    }
}