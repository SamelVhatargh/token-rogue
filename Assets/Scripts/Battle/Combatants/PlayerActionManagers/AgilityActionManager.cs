using System;
using Battle.CombatActions;
using Battle.Tokens;
using Battle.Tokens.Selections;
using Ui;

namespace Battle.Combatants.PlayerActionManagers
{
    public class AgilityActionManager : AbstractActionManager
    {
        private Selection _playerSelection;
        private Selection _enemySelection;
        private ICombatAction _action;

        public override void Init(Selection tokenSelection, Stats attacker, Stats defender,
            ActionHelperController actionHelper,
            SelectionManager selectionManager)
        {
            base.Init(tokenSelection, attacker, defender, actionHelper, selectionManager);
            SetMessage("Select your token to flip or opponent to recast");
            actionHelper.DisableConfirmButton();

            _playerSelection = new Selection(Symbol.Everything);
            _playerSelection.ForbidSpecificTokens(tokenSelection.GetSelectedTokens());
            _playerSelection.SetLimit(1);
            _enemySelection = new Selection(Symbol.Everything);

            _playerSelection.OnSelectionChanged += PlayerSelection_OnSelectionChanged;
            _enemySelection.OnSelectionChanged += EnemySelectionSelection_OnSelectionChanged;

            selectionManager.SetSelections(_playerSelection, _enemySelection);
        }

        private void EnemySelectionSelection_OnSelectionChanged()
        {
            if (_enemySelection.GetSelectedTokens().Count == 1)
            {
                _selectionManager.SetSelections(new Selection(Symbol.None), _enemySelection);
                SetMessage("Press confirm to recast opponent token.");
                _action = new RecastAction(
                    _enemySelection.GetSelectedTokens()[0],
                    _attacker,
                    _defender,
                    _tokenSelection.GetSelectedTokens()
                );
                _actionHelper.EnableConfirmButton();
                return;
            }
            
            _selectionManager.SetSelections(_playerSelection, _enemySelection);
            
            SetMessage("Select your token to flip or opponent to recast");
            _actionHelper.DisableConfirmButton();
        }

        private void PlayerSelection_OnSelectionChanged()
        {
            if (_playerSelection.GetSelectedTokens().Count == 1)
            {
                _selectionManager.SetSelections(_playerSelection, new Selection(Symbol.None));
                SetMessage("Press confirm to flip your token.");
                _action = new FlipAction(
                    _playerSelection.GetSelectedTokens()[0],
                    _attacker,
                    _attacker,
                    _tokenSelection.GetSelectedTokens()
                );
                _actionHelper.EnableConfirmButton();
                return;
            }
            
            _selectionManager.SetSelections(_playerSelection, _enemySelection);
            
            SetMessage("Select your token to flip or opponent to recast");
            _actionHelper.DisableConfirmButton();
        }

        protected override void HandleSelectionChange()
        {
        }

        protected override void HandleConfirm()
        {
            InvokeActionReady(_action);
        }

        public override Tuple<Selection, Selection> getSelections()
        {
            return new Tuple<Selection, Selection>(
                new Selection(Symbol.Agility),
                new Selection(Symbol.None)
            );
        }

        public override void Clear()
        {
            base.Clear();
            _playerSelection.OnSelectionChanged -= PlayerSelection_OnSelectionChanged;
            _enemySelection.OnSelectionChanged -= EnemySelectionSelection_OnSelectionChanged;
        }
    }
}