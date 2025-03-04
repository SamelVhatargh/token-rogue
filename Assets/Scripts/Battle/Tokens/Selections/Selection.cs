using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Tokens.Selections
{
    public class Selection
    {
        public event Action OnSelectionChanged;

        private readonly Symbol _validSymbols;
        private readonly bool _allowSpentTokens;
        private readonly List<Token> _selectedTokens;
        private List<Token> _forbiddenTokens;
        private int _limit;

        public Selection(Symbol validSymbols, bool allowSpentTokens = false)
        {
            _validSymbols = validSymbols;
            _allowSpentTokens = allowSpentTokens;
            _selectedTokens = new List<Token>();
            _forbiddenTokens = new List<Token>();
            _limit = 0;
        }

        public List<Token> GetSelectedTokens()
        {
            return _selectedTokens;
        }

        public List<Token> GetSelectableTokens(List<Token> tokens)
        {
            return tokens.Where(CanTokenBeAdded).ToList();
        }

        public bool ToggleToken(Token token)
        {
            return _selectedTokens.Contains(token) ? RemoveToken(token) : AddToken(token);
        }

        private bool AddToken(Token token)
        {
            if (!CanTokenBeAdded(token))
            {
                return false;
            }

            _selectedTokens.Add(token);
            OnSelectionChanged?.Invoke();
            return true;
        }

        private bool CanTokenBeAdded(Token token)
        {
            if (!_allowSpentTokens && token.IsSpent)
            {
                return false;
            }
            
            if (_forbiddenTokens.Contains(token))
            {
                return false;
            }

            if (_limit > 0 && _selectedTokens.Count >= _limit)
            {
                return false;
            }

            return (_validSymbols & token.ActiveSide.Symbol) != 0;
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

        public void ForbidSpecificTokens(List<Token> tokens)
        {
            _forbiddenTokens = tokens;
        }
        
        public void SetLimit(int limit)
        {
            _limit = limit;
        }
    }
}