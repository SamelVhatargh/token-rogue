using System;
using System.Linq;
using Battle.CombatActions;
using Battle.Tokens;
using Battle.Tokens.Selections;
using Ui;

namespace Battle.Combatants.PlayerActionManagers
{
    public class AttackActionManager : AbstractActionManager
    {
        public override void Init(Selection tokenSelection, Stats attacker, Stats defender, ActionHelperController actionHelper, SelectionManager selectionManager)
        {
            base.Init(tokenSelection, attacker, defender, actionHelper, selectionManager);
            SetMessage("Add more ⚔️ tokens or press confirm to attack.");
        }

        public override Tuple<Selection, Selection> getSelections()
        {
            return new Tuple<Selection, Selection>(
                new Selection(Symbol.Attack),
                new Selection(Symbol.None)
            );
        }

        protected override void HandleSelectionChange()
        {
            if (_attacker.Tokens.CombatPool.Count(token => token.ActiveSide.Symbol == Symbol.Attack)
                == _tokenSelection.GetSelectedTokens().Count)
            {
                SetMessage("Press confirm to attack.");
                return;
            }

            SetMessage("Add more ⚔️ tokens or press confirm to attack.");
        }

        protected override void HandleConfirm()
        {
            var tokens = _tokenSelection.GetSelectedTokens();
            var damage = tokens.Sum(token => token.ActiveSide.Value);
            var action = new AttackAction(_attacker, _defender, damage, tokens);
            InvokeActionReady(action);
        }
    }
}