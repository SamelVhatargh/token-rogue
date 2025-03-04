using System;
using Battle.CombatActions;
using Battle.Tokens.Selections;

namespace Battle.Combatants.PlayerActionManagers
{
    public interface IActionManager
    {
        event Action<ICombatAction> OnActionReady;
        void Init(Selection tokenSelection, Stats attacker, Stats defender);
        void Clear();
    }
}