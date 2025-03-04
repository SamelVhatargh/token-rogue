using System;
using System.Collections.Generic;
using Battle.Tokens.Selections;
using UnityEngine;

namespace Battle.Tokens
{
    public class TokenPoolView : MonoBehaviour
    {
        [SerializeField] private GameObject tokenPrefab;

        private TokenPool _tokens;
        private readonly Dictionary<Token, TokenVisualState> _tokenVisualStates = new();

        public event Action<Token> OnTokenClicked;

        public void SetTokens(TokenPool tokens)
        {
            _tokens = tokens;
            Render();
            _tokens.OnCombatPoolUpdated += Render;
            _tokens.OnSpentPoolUpdated += Render;
        }

        private void Render()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            var offset = Vector3.zero;
            RenderPool(_tokens.CombatPool, offset);

            offset = new Vector3(0, -1.5f, 0);
            RenderPool(_tokens.SpentPool, offset);
        }

        private void RenderPool(List<Token> pool, Vector3 offset)
        {
            foreach (var token in pool)
            {
                var tokenInstance = Instantiate(tokenPrefab, offset, Quaternion.identity);
                var tokenView = tokenInstance.GetComponent<TokenView>();
                tokenInstance.transform.SetParent(transform, false);
                tokenView.SetToken(token);
                if (_tokenVisualStates.TryGetValue(token, out var state))
                {
                    tokenView.SetState(state);
                }
                tokenView.OnTokenClicked += clickedToken => OnTokenClicked?.Invoke(clickedToken);
                offset += new Vector3(1.5f, 0, 0);
            }
        }

        public void UpdateVisualStates(Selection tokenSelection)
        {
            _tokenVisualStates.Clear();
            _tokens.All.ForEach(token => _tokenVisualStates[token] = TokenVisualState.Normal);

            var selectableTokens = tokenSelection.GetSelectableTokens(_tokens.All);
            _tokens.All.ForEach(token =>
            {
                _tokenVisualStates[token] = selectableTokens.Contains(token)
                    ? TokenVisualState.Selectable
                    : TokenVisualState.NonSelectable;
            });

            var selectedTokens = tokenSelection.GetSelectedTokens();
            selectedTokens.ForEach(token => _tokenVisualStates[token] = TokenVisualState.Selected);
            Render();
        }
    }
}