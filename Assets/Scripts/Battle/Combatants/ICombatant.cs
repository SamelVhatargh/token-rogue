using System;
using Battle.CombatActions;

namespace Battle.Combatants
{
    public interface ICombatant
    {
        event Action<ICombatAction> OnActionTaken;
        
        public void DoCombatAction();
    }
}