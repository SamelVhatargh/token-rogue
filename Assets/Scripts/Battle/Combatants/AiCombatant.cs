using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.CombatActions;
using Battle.Tokens;
using UnityEngine;

namespace Battle.Combatants
{
    public class AiCombatant : MonoBehaviour, ICombatant
    {
        [SerializeField] private TokenPoolView tokenPoolView;

        private ICombatant _opponent;

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


            var attackTokens = Stats.Tokens.CombatPool
                .Where(token => token.ActiveSide.Symbol == Symbol.Attack)
                .OrderByDescending(token => token.ActiveSide.Value)
                .ToList();

            if (attackTokens.Count == 0)
            {
                OnActionTaken?.Invoke(new SkipAction(Stats.Name));
            }
            else
            {
                var token = attackTokens.First();
                var attackAction = new AttackAction(
                    Stats,
                    _opponent.Stats,
                    token.ActiveSide.Value,
                    new List<Token> { token }
                );
                OnActionTaken?.Invoke(attackAction);
            }
        }

        public void SetOpponent(ICombatant opponent)
        {
            _opponent = opponent;
        }

        public override string ToString()
        {
            return "AiCombatant";
        }
    }
}