using System;
using System.Collections.Generic;
using Battle.CombatActions;
using Battle.Tokens;
using UnityEngine;

namespace Battle.Combatants
{
    public class Player : MonoBehaviour, ICombatant
    {
        [SerializeField] private TokenPoolView tokenPoolView;
        
        public event Action<ICombatAction> OnActionTaken;
        public Stats Stats { get; private set; }

        private bool _isPlayerTurn;
        
        private void Update()
        {
            if (!_isPlayerTurn)
            {
                return;
            }
            
            // if (Input.GetKeyDown(KeyCode.C))
            // {
            //     Tokens.Cast();
            //     return;
            // }
            //
            // if (!Input.GetKeyDown(KeyCode.Space))
            // {
            //     return;
            // }
            return;

            var action = new TestCombatAction();
            OnActionTaken?.Invoke(action);
            _isPlayerTurn = false;
        }

        public void SetStats(Stats stats)
        {
            Stats = stats;
            tokenPoolView.Render(stats.Tokens);
        }

        public void DoCombatAction()
        {
            _isPlayerTurn = true;
        }

        public override string ToString()
        {
            return "Player";
        }
    }
}