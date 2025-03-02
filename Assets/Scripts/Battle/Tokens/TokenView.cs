using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Battle.Tokens
{
    public class TokenView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private SpriteRenderer faceValue;
        [SerializeField] private SpriteRenderer backValue;
      
        private Token _token;
        
        public event Action<Token> OnTokenClicked;
        
        public void setToken(Token token)
        {
            if (_token != null)
            {
                _token.SideChanged -= UpdateSprites;
            }
            _token = token;
            UpdateSprites();
            _token.SideChanged += UpdateSprites;
        }

        private void UpdateSprites()
        {
            faceValue.sprite = ResourceCache.LoadTokenSprite(_token.ActiveSide.Symbol);
            backValue.sprite = ResourceCache.LoadTokenSprite(_token.InactiveSide.Symbol);
        }

        private void OnDisable()
        {
            if (_token != null)
            {
                _token.SideChanged -= UpdateSprites;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnTokenClicked?.Invoke(_token);
        }
    }
}