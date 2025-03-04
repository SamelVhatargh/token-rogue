using System.Collections.Generic;
using Battle.Combatants;

namespace Battle
{
    public class TurnOrder
    {
        private readonly List<ICombatant> _combatants;
        private int _currentCombatantIndex;
        private bool _isFirstTurn;

        public TurnOrder(List<ICombatant> combatants)
        {
            _isFirstTurn = true;
            _currentCombatantIndex = 0;
            _combatants = combatants;
        }
        
        public void RemoveCurrentCombatant()
        {
            var nextCombatant = _combatants[GetNextCombatantIndex()];
            _combatants.RemoveAt(_currentCombatantIndex);
            _currentCombatantIndex = _combatants.IndexOf(nextCombatant);
        }
        
        public ICombatant Current()
        {
            return _combatants.Count == 0 ? null : _combatants[_currentCombatantIndex];
        }

        public ICombatant Advance()
        {
            _currentCombatantIndex = _isFirstTurn ? 0 : GetNextCombatantIndex();
            _isFirstTurn = false;
            return _combatants[_currentCombatantIndex];
        }
        
        private int GetNextCombatantIndex()
        {
            return (_currentCombatantIndex + 1) % _combatants.Count;
        }
        
        public bool isEmpty()
        {
            return _combatants.Count == 0;
        }
    }
}