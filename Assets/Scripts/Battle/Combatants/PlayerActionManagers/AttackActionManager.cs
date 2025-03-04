using System;
using System.Linq;
using Battle.CombatActions;
using Battle.Tokens;
using Battle.Tokens.Selections;
using Ui;
using UnityEngine;

namespace Battle.Combatants.PlayerActionManagers
{
    public class AttackActionManager : MonoBehaviour, IActionManager
    {
        [SerializeField] private ActionHelperController actionHelper;
        
        public event Action<ICombatAction> OnActionReady;
        
        private Selection _tokenSelection;
        private Stats _attacker;
        private Stats _defender;
        
        public void Init(Selection tokenSelection, Stats attacker, Stats defender)
        {
            _tokenSelection = tokenSelection;
            _attacker = attacker;
            _defender = defender;
            actionHelper.SetMessage("Add more ⚔️ tokens or press confirm to attack.");
            _tokenSelection.OnSelectionChanged += TokenSelection_OnSelectionChanged; 

            actionHelper.EnableConfirmButton();
            actionHelper.OnConfirmClicked += ActionHelper_OnConfirmClicked;
        }

        public void Clear()
        {
            actionHelper.OnConfirmClicked -= ActionHelper_OnConfirmClicked;
            _tokenSelection.OnSelectionChanged -= TokenSelection_OnSelectionChanged;
            
            _tokenSelection = null;
            _attacker = null;
            _defender = null;
        }

        private void TokenSelection_OnSelectionChanged()
        {
            if (_attacker.Tokens.CombatPool.Count(token => token.ActiveSide.Symbol == Symbol.Attack)
                == _tokenSelection.GetSelectedTokens().Count)
            {
                actionHelper.SetMessage("Press confirm to attack.");
                return;
            }
            actionHelper.SetMessage("Add more ⚔️ tokens or press confirm to attack.");
        }

        private void ActionHelper_OnConfirmClicked()
        {
            actionHelper.OnConfirmClicked -= ActionHelper_OnConfirmClicked;
            _tokenSelection.OnSelectionChanged -= TokenSelection_OnSelectionChanged; 

            var tokens = _tokenSelection.GetSelectedTokens();
            var damage = tokens.Sum(token => token.ActiveSide.Value);
            
            var action = new AttackAction(_attacker, _defender, damage, tokens);
            OnActionReady?.Invoke(action);
        }

        private void OnDisable()
        {
            if (actionHelper != null)
            {
                actionHelper.OnConfirmClicked -= ActionHelper_OnConfirmClicked;
            }
        
            if (_tokenSelection != null)
            {
                _tokenSelection.OnSelectionChanged -= TokenSelection_OnSelectionChanged;
            }
        }
    }
}