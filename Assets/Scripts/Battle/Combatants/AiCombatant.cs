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
        public CombatantType Type => CombatantType.Enemy;

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

            var agilityTokens = Stats.Tokens.CombatPool
                .Where(token => token.ActiveSide.Symbol == Symbol.Agility)
                .ToList();

            if (attackTokens.Count == 0)
            {
                if (agilityTokens.Count == 0)
                {
                    OnActionTaken?.Invoke(new SkipAction(Stats.Name));
                }
                else
                {
                    var usedAgilityToken = agilityTokens.First();
                    var flippedAttackToken = Stats.Tokens.CombatPool.FirstOrDefault(
                        token => token.InactiveSide.Symbol == Symbol.Attack && token != usedAgilityToken
                    );
                    if (flippedAttackToken != null)
                    {
                        var action = new FlipAction(
                            flippedAttackToken,
                            Stats,
                            Stats,
                            new List<Token> { usedAgilityToken }
                        );
                        OnActionTaken?.Invoke(action);
                    }
                    else
                    {
                        var opponentTokens = _opponent.Stats.Tokens.CombatPool;
                        if (opponentTokens.Count == 0)
                        {
                            OnActionTaken?.Invoke(new SkipAction(Stats.Name));
                        }
                        else
                        {
                            var randomOpponentToken = opponentTokens[UnityEngine.Random.Range(0, opponentTokens.Count)];
                            var action = new RecastAction(
                                randomOpponentToken,
                                Stats,
                                _opponent.Stats,
                                new List<Token> { usedAgilityToken }
                            );
                            OnActionTaken?.Invoke(action);
                        }
                    }
                }
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
            return Stats.Name;
        }
    }
}