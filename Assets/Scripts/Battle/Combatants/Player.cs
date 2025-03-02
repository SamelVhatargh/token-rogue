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
            
            if (Stats.Tokens.IsSpent(token))
            {
                return;
            }

            var tokenSide = token.ActiveSide;
            if (tokenSide.Symbol != Symbol.Attack)
            {
                return;
            }

            OnActionTaken?.Invoke(
                new AttackAction(Stats, _opponent.Stats, tokenSide.Value, new List<Token> { token })
            );
            _isPlayerTurn = false;
        }

        public void SetStats(Stats stats)
        {
            Stats = stats;
            tokenPoolView.SetTokens(stats.Tokens);
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