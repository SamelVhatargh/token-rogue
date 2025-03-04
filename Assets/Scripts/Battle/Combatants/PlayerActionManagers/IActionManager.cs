using System;
using Battle.CombatActions;
using Battle.Tokens.Selections;
using Ui;

namespace Battle.Combatants.PlayerActionManagers
{
    public interface IActionManager
    {
        event Action<ICombatAction> OnActionReady;

        void Init(Selection tokenSelection, Stats attacker, Stats defender, ActionHelperController actionHelper,
            SelectionManager selectionManager);

        void Clear();
        Tuple<Selection, Selection> getSelections();
    }
}