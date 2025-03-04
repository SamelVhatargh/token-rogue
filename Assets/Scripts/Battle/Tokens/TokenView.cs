using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Battle.Tokens
{
    public class TokenView : MonoBehaviour, IPointerDownHandler
    {
        private static readonly Color InitiativeColor = new(1, 0.3f, 0);
        private static readonly Color DefaultColor = Color.black;

        [SerializeField] private TextMeshPro faceValue;
        [SerializeField] private SpriteRenderer faceSymbol;
        [SerializeField] private TextMeshPro backValue;
        [SerializeField] private SpriteRenderer backSymbol;

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
            UpdateSide(faceSymbol, faceValue, _token.ActiveSide);
            UpdateSide(backSymbol, backValue, _token.InactiveSide);
        }

        private static void UpdateSide(SpriteRenderer symbolSpriteRenderer, TextMeshPro text, Side side)
        {
            var color = side.HasInitiative ? InitiativeColor : DefaultColor;
            var symbol = side.Value == 0 ? Symbol.Empty : side.Symbol;
            var textValue = side.Value > 1 ? side.Value.ToString() : "";
        
            symbolSpriteRenderer.sprite = ResourceCache.LoadTokenSprite(symbol);
            symbolSpriteRenderer.color = color;
        
            text.text = textValue;
            text.color = color;
            text.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, color);
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