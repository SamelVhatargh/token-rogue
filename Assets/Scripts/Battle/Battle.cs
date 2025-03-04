using System;
using System.Collections.Generic;
using Battle.CombatActions;
using Battle.Combatants;

namespace Battle
{
    public class Battle
    {
        public event Action<ICombatAction> OnCombatActionExecuted;
        public event Action<int, ICombatant> OnRoundStarted;

        private readonly ICombatant _combatantA;
        private readonly ICombatant _combatantB;
        private int _currentRound;
        private TurnOrder _turnOrder;

        public Battle(ICombatant combatantA, ICombatant combatantB)
        {
            _combatantA = combatantA;
            _combatantB = combatantB;
        }

        public void Start()
        {
            _currentRound = 0;
            StartNextRound();
        }

        private void StartNextRound()
        {
            _currentRound++;

            _combatantA.Stats.Tokens.Cast();
            _combatantB.Stats.Tokens.Cast();

            var combatants = new List<ICombatant> { _combatantA, _combatantB };
            combatants.Sort((a, b) =>
                b.Stats.Tokens.GetInitiative().CompareTo(a.Stats.Tokens.GetInitiative())
            );
            _turnOrder = new TurnOrder(combatants);
            
            OnRoundStarted?.Invoke(_currentRound, combatants[0]);
            ProcessTurn();
        }

        private void ProcessTurn()
        {
            if (_turnOrder.isEmpty())
            {
                StartNextRound();
                return;
            }

            var combatant = _turnOrder.Advance();
            combatant.OnActionTaken += ExecuteAction;

            combatant.DoCombatAction();
        }

        private void ExecuteAction(ICombatAction action)
        {
            _turnOrder.Current().OnActionTaken -= ExecuteAction;

            if (action is SkipAction)
            {
                _turnOrder.RemoveCurrentCombatant();
            }

            action.Execute();
            OnCombatActionExecuted?.Invoke(action);

            ProcessTurn();
        }
    }
}