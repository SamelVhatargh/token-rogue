using System;
using Battle.CombatActions;
using Battle.Tokens.Selections;
using Ui;
using UnityEngine;

namespace Battle.Combatants.PlayerActionManagers
{
    public abstract class AbstractActionManager : IActionManager
    {
        public event Action<ICombatAction> OnActionReady;

        protected Selection _tokenSelection;
        protected Stats _attacker;
        protected Stats _defender;
        protected ActionHelperController _actionHelper;
        protected SelectionManager _selectionManager;

        public virtual void Init(Selection tokenSelection, Stats attacker, Stats defender, ActionHelperController actionHelper,
            SelectionManager selectionManager)
        {
            _tokenSelection = tokenSelection;
            _attacker = attacker;
            _defender = defender;
            _actionHelper = actionHelper;
            _selectionManager = selectionManager;
            
            _tokenSelection.OnSelectionChanged += TokenSelection_OnSelectionChanged;

            _actionHelper.EnableConfirmButton();
            _actionHelper.EnableCancelButton();
            _actionHelper.OnConfirmClicked += ActionHelper_OnConfirmClicked;
        }

        private void TokenSelection_OnSelectionChanged()
        {
            HandleSelectionChange();
        }

        protected abstract void HandleSelectionChange();

        private void ActionHelper_OnConfirmClicked()
        {
            _actionHelper.OnConfirmClicked -= ActionHelper_OnConfirmClicked;
            _tokenSelection.OnSelectionChanged -= TokenSelection_OnSelectionChanged;

            HandleConfirm();
        }

        protected abstract void HandleConfirm();

        public virtual void Clear()
        {
            _actionHelper.OnConfirmClicked -= ActionHelper_OnConfirmClicked;
            _tokenSelection.OnSelectionChanged -= TokenSelection_OnSelectionChanged;

            _tokenSelection = null;
            _attacker = null;
            _defender = null;
            _actionHelper = null;
            _selectionManager = null;
        }

        public abstract Tuple<Selection, Selection> getSelections();
        
        protected void SetMessage(string message)
        {
            _actionHelper.SetMessage(message);
        }
        
        protected void InvokeActionReady(ICombatAction action)
        {
            OnActionReady?.Invoke(action);
        }
    }
}