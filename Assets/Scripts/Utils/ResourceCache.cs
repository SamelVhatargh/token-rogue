using System.Collections.Generic;
using Battle.Tokens;
using UnityEngine;

namespace Utils
{
    public static class ResourceCache
    {
        private static readonly Dictionary<Symbol, Sprite> _tokenSpriteCache = new();

        public static Sprite LoadTokenSprite(Symbol symbol)
        {
            if (_tokenSpriteCache.TryGetValue(symbol, out var cachedSprite))
            {
                return cachedSprite;
            }

            var path = $"Sprites/Tokens/{symbol}";

            var loadedSprite = Resources.Load<Sprite>(path);
            if (loadedSprite != null)
            {
                _tokenSpriteCache[symbol] = loadedSprite;
            }
            else
            {
                Debug.LogWarning($"Sprite {symbol} could not be loaded from path: {path}");
            }
            return loadedSprite;
        }
    }
}