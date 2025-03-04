using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Battle.Tokens
{
    public class TokenView : MonoBehaviour, IPointerDownHandler
    {
        private static readonly Color InitiativeColor = new(1, 0.3f, 0);
        private static readonly Color DefaultColor = Color.black;
        
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
            UpdateSide(faceValue, _token.ActiveSide);
            UpdateSide(backValue, _token.InactiveSide);
        }

        private static void UpdateSide(SpriteRenderer spriteRenderer, Side side)
        {
            spriteRenderer.sprite = ResourceCache.LoadTokenSprite(side.Symbol);
            spriteRenderer.color = side.HasInitiative ? InitiativeColor : DefaultColor;
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