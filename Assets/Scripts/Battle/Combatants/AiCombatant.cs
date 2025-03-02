using System;
using System.Collections;
using System.Collections.Generic;
using Battle.CombatActions;
using Battle.Tokens;
using UnityEngine;

namespace Battle.Combatants
{
    public class AiCombatant : MonoBehaviour, ICombatant
    {
        [SerializeField] private TokenPoolView tokenPoolView;

        public event Action<ICombatAction> OnActionTaken;
        public Stats Stats { get; private set; }

        public void SetStats(Stats stats)
        {
            Stats = stats;
            tokenPoolView.SetTokens(stats.Tokens);
        }

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