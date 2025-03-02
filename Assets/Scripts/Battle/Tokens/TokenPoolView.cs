using UnityEngine;

namespace Battle.Tokens
{
    public class TokenPoolView : MonoBehaviour
    {
        [SerializeField] private GameObject tokenPrefab;
        
        public void Render(TokenPool tokens)
        {
            var offset = Vector3.zero;
            foreach (var token in tokens.All)
            {
                var tokenInstance = Instantiate(tokenPrefab, offset, Quaternion.identity, transform);
                var tokenView = tokenInstance.GetComponent<TokenView>();
                tokenView.setToken(token);
                offset += new Vector3(1.5f, 0, 0);
            }
        }
    }
}