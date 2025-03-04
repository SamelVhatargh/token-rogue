using System;
using System.Collections.Generic;
using System.Linq;
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

        private void Update()
        {
            if (!_isPlayerTurn)
            {
                return;
            }

            if (Stats.Tokens.CombatPool.Count(token => token.ActiveSide.Symbol == Symbol.Attack) != 0)
            {
                return;
            }

            _isPlayerTurn = false;
            OnActionTaken?.Invoke(new SkipAction(Stats.Name));
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

            _isPlayerTurn = false;
            OnActionTaken?.Invoke(
                new AttackAction(Stats, _opponent.Stats, tokenSide.Value, new List<Token> { token })
            );
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
            return Stats.Name;
        }
    }
}