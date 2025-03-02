using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Tokens
{
    public class TokenPoolView : MonoBehaviour
    {
        [SerializeField] private GameObject tokenPrefab;

        private TokenPool _tokens;
        
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
                tokenView.setToken(token);
                tokenView.OnTokenClicked += clickedToken => OnTokenClicked?.Invoke(clickedToken);
                offset += new Vector3(1.5f, 0, 0);
            }
        }
    }
}