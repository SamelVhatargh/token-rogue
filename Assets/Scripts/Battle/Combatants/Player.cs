using System;
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
        
        private ICombatant _opponent;
        private bool _isPlayerTurn;

        private void OnEnable()
        {
            tokenPoolView.OnTokenClicked += TokenPoolView_OnTokenClicked;
        }

        private void TokenPoolView_OnTokenClicked(Token token)
        {
            if (!_isPlayerTurn)
            {
                return;
            }

            var tokenSide = token.ActiveSide;
            if (tokenSide.Symbol != Symbol.Attack)
            {
                return;
            }
            
            OnActionTaken?.Invoke(new AttackAction(_opponent.Stats, tokenSide.Value));
            _isPlayerTurn = false;
        }

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
        
        public void SetOpponent(ICombatant opponent)
        {
            _opponent = opponent;
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