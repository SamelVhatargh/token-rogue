using System;
using Battle.CombatAction;

namespace Battle.Combatant
{
    public interface ICombatant
    {
        event Action<ICombatAction> OnActionTaken;
        
        public void DoCombatAction();
    }
}