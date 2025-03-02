using Battle.CombatActions;
using Battle.Combatants;

namespace Battle
{
    public class Battle
    {
        public ICombatant CurrentCombatant { get; private set; }

        private ICombatant _combatantA;
        private ICombatant _combatantB;
        private int _currentRound;

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
            CurrentCombatant = null;
            SetCurrentTurn(_combatantA);
        }

        private void SetCurrentTurn(ICombatant combatant)
        {
            var previousCombatant = CurrentCombatant;
            if (previousCombatant != null)
            {
                previousCombatant.OnActionTaken -= ExecuteAction;
            }

            CurrentCombatant = combatant;
            CurrentCombatant.OnActionTaken += ExecuteAction;
            CurrentCombatant.DoCombatAction();
        }

        private void ExecuteAction(ICombatAction action)
        {
            action.Execute();
            SetCurrentTurn(NextCombatant());
        }

        private ICombatant NextCombatant()
        {
            return CurrentCombatant == _combatantA ? _combatantB : _combatantA;
        }
    }
}