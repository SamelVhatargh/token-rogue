using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Tokens.Selections
{
    public class Selection
    {
        public event Action OnSelectionChanged;
        
        private readonly Symbol _validSymbols;
        private readonly List<Token> _selectedTokens;

        public Selection(Symbol validSymbols)
        {
            _validSymbols = validSymbols;
            _selectedTokens = new List<Token>();
        }

        public List<Token> GetSelectedTokens()
        {
            return _selectedTokens;
        }

        public List<Token> GetSelectableTokens(List<Token> tokens)
        {
            return tokens.Where(token => (_validSymbols & token.ActiveSide.Symbol) != 0).ToList();
        }

        public bool ToggleToken(Token token)
        {
            return _selectedTokens.Contains(token) ? RemoveToken(token) : AddToken(token);
        }

        private bool AddToken(Token token)
        {
            if ((_validSymbols & token.ActiveSide.Symbol) == 0)
            {
                return false;
            }

            _selectedTokens.Add(token);
            OnSelectionChanged?.Invoke();
            return true;
        }

        private bool RemoveToken(Token token)
        {
            var result = _selectedTokens.Remove(token);
            if (result)
            {
                OnSelectionChanged?.Invoke();
            }
            return result;
        }
        
        public bool IsEmpty()
        {
            return GetSelectedTokens().Count == 0;
        }
    }
}