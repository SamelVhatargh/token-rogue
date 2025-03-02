using UnityEngine;
using Utils;

namespace Battle.Tokens
{
    public class TokenView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer faceValue;
        [SerializeField] private SpriteRenderer backValue;
      
        private Token _token;
        
        public void setToken(Token token)
        {
            _token = token;
            UpdateSprites();
        }

        private void UpdateSprites()
        {
            faceValue.sprite = ResourceCache.LoadTokenSprite(_token.ActiveSide.Symbol);
            backValue.sprite = ResourceCache.LoadTokenSprite(_token.InactiveSide.Symbol);
        }
    }
}