using System;
using System.Collections.Generic;
using System.Linq;
using Battle.CombatActions;
using Battle.Combatants.PlayerActionManagers;
using Battle.Tokens;
using Battle.Tokens.Selections;
using JetBrains.Annotations;
using Ui;
using UnityEngine;

namespace Battle.Combatants
{
    public class Player : MonoBehaviour, ICombatant
    {
        [SerializeField] private TokenPoolView tokenPoolView;
        [SerializeField] private SelectionManager selectionManager;
        [SerializeField] private ActionHelperController actionHelper;

        public event Action<ICombatAction> OnActionTaken;
        public Stats Stats { get; private set; }
        public CombatantType Type => CombatantType.Player;

        private ICombatant _opponent;
        private bool _isPlayerTurn;
        
        private Selection _playerSelection;
        private Selection _enemySelection;

        private IActionManager _currentActionManager;
        private Battle _battle;

        private void OnEnable()
        {
            selectionManager.OnTokenToggled += SelectionManager_OnTokenToggled;
            actionHelper.OnCancelClicked += ActionHelper_OnCancelClicked;
        }

        private void Battle_OnRoundStarted(int roundCount, ICombatant firstCombatant)
        {
            ResetActionManager();
        }

        private void ActionHelper_OnCancelClicked()
        {
            ResetActionManager();
        }

        private void Update()
        {
            if (!_isPlayerTurn)
            {
                return;
            }

            if (Stats.Tokens.CombatPool.Count(token => token.ActiveSide.Symbol == Symbol.Attack) != 0)
            {
                return;
            }

            _isPlayerTurn = false;
            OnActionTaken?.Invoke(new SkipAction(Stats.Name));
        }

        private void SelectionManager_OnTokenToggled(Token token)
        {
            if (_currentActionManager == null)
            {
                switch (token.ActiveSide.Symbol)
                {
                    case Symbol.Attack:
                        _currentActionManager = new AttackActionManager();
                        SetCombatActionSelection();
                        _playerSelection.ToggleToken(token);
                        _currentActionManager.Init(_playerSelection, Stats, _opponent.Stats, actionHelper, selectionManager);
                        break;
                    case Symbol.Agility:
                        _currentActionManager = new AgilityActionManager();
                        var selections = _currentActionManager.getSelections();
                        _playerSelection = selections.Item1;
                        _enemySelection = selections.Item2;
                        _playerSelection.ToggleToken(token);
                        _currentActionManager.Init(_playerSelection, Stats, _opponent.Stats, actionHelper, selectionManager);
                        break;
                    case Symbol.None:
                    case Symbol.Empty:
                    case Symbol.Defense:
                    case Symbol.Energy:
                    case Symbol.Everything:
                    default:
                        Debug.LogWarning($"Action manager for {token.ActiveSide.Symbol} not implemented yet.");
                        break;
                }
                if (_currentActionManager != null)
                {
                    _currentActionManager.OnActionReady += CurrentActionManager_OnActionReady;
                }
            }

            if (_currentActionManager != null && _playerSelection.IsEmpty() && _enemySelection.IsEmpty())
            {
                ResetActionManager();
            }
        }

        private void CurrentActionManager_OnActionReady(ICombatAction action)
        {
            _isPlayerTurn = false;
            OnActionTaken?.Invoke(action);
            ResetActionManager();
        }

        public void SetStats(Stats stats)
        {
            Stats = stats;
            tokenPoolView.SetTokens(stats.Tokens);
        }

        public void SetOpponent(ICombatant opponent)
        {
            _opponent = opponent;
        }
        
        public void setBattle(Battle battle)
        {
            _battle = battle;
            _battle.OnRoundStarted += Battle_OnRoundStarted;
        }

        public void DoCombatAction()
        {
            _isPlayerTurn = true;
        }
        
        public void SetDefaultSelection()
        {
            _playerSelection = new Selection(Symbol.Everything);
            _enemySelection = new Selection(Symbol.None);
            UpdateSelectionManager();
        }
        
        [Obsolete]
        private void SetCombatActionSelection()
        {
            _playerSelection = new Selection(Symbol.Attack);
            _enemySelection = new Selection(Symbol.None);
            UpdateSelectionManager();
        }

        public void ResetActionManager()
        {
            if (_currentActionManager != null)
            {
                _currentActionManager.Clear();
                _currentActionManager.OnActionReady -= CurrentActionManager_OnActionReady;
            }
            _currentActionManager = null;
            SetDefaultSelection();
            actionHelper.SetMessage("Select tokens to act.");
            actionHelper.DisableCancelButton();
            actionHelper.DisableConfirmButton();
        }

        private void UpdateSelectionManager()
        {
            selectionManager.SetSelections(_playerSelection, _enemySelection);
        }

        public override string ToString()
        {
            return Stats.Name;
        }
    }
}