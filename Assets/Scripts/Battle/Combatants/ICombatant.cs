﻿using System;
using Battle.CombatActions;
using Battle.Tokens;

namespace Battle.Combatants
{
    public interface ICombatant
    {
        event Action<ICombatAction> OnActionTaken;
        
        public Stats Stats { get; }
        
        public void DoCombatAction();
    }
}