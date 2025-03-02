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

        private void Awake()
        {
            var tokens = new TokenPool(new List<Token>
            {
                new(new Side(2, Symbol.Attack), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Energy), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Attack), new Side(0, Symbol.None)),
                new(new Side(1, Symbol.Agility), new Side(0, Symbol.None)),
            });
            Stats = new Stats(5, tokens);
            tokenPoolView.Render(tokens);
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