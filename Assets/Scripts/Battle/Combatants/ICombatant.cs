using System;
using Battle.CombatActions;
using Battle.Tokens;

namespace Battle.Combatants
{
    public interface ICombatant
    {
        event Action<ICombatAction> OnActionTaken;
        
        public TokenPool Tokens { get; }
        
        public void DoCombatAction();
    }
}