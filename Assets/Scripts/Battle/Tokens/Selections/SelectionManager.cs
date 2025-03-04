using System;
using UnityEngine;

namespace Battle.Tokens.Selections
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private TokenPoolView playerTokenPoolView;
        [SerializeField] private TokenPoolView enemyTokenPoolView;
        
        public event Action<Token> OnTokenToggled;
        
        private Selection _playerTokenSelection;
        private Selection _enemyTokenSelection;

        private void OnEnable()
        {
            playerTokenPoolView.OnTokenClicked += PlayerTokenPoolView_OnTokenClicked;
            enemyTokenPoolView.OnTokenClicked += EnemyTokenPoolView_OnTokenClicked;
        }

        private void EnemyTokenPoolView_OnTokenClicked(Token token)
        {
            HandleTokenClick(token, _enemyTokenSelection);
        }

        private void PlayerTokenPoolView_OnTokenClicked(Token token)
        {
            HandleTokenClick(token, _playerTokenSelection);
            UpdateTokenVisuals();
        }

        private void HandleTokenClick(Token token, Selection tokenSelection)
        {
            var toggled = tokenSelection.ToggleToken(token);
            if (!toggled)
            {
                return;
            }

            OnTokenToggled?.Invoke(token);
        }

        public void SetSelections(Selection playerTokenSelection, Selection enemyTokenSelection)
        {
            _playerTokenSelection = playerTokenSelection;
            _enemyTokenSelection = enemyTokenSelection;
            UpdateTokenVisuals();
        }

        private void UpdateTokenVisuals()
        {
            playerTokenPoolView.UpdateVisualStates(_playerTokenSelection);
            enemyTokenPoolView.UpdateVisualStates(_enemyTokenSelection);
        }

        private void OnDisable()
        {
            playerTokenPoolView.OnTokenClicked -= PlayerTokenPoolView_OnTokenClicked;
            enemyTokenPoolView.OnTokenClicked -= EnemyTokenPoolView_OnTokenClicked;
        }
    }
}