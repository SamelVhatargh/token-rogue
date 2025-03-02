using System;
using Battle.Combatants;
using UnityEngine;

namespace Battle.Tokens
{
    public class TokenPoolView : MonoBehaviour
    {
        [SerializeField] private GameObject tokenPrefab;
        
        public event Action<Token> OnTokenClicked;

        public void Render(TokenPool tokens)
        {
            var offset = Vector3.zero;
            foreach (var token in tokens.All)
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