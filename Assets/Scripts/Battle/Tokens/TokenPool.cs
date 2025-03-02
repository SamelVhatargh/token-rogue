using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Tokens
{
    public class TokenPool
    {
        public List<Token> All { get; private set; }
        public List<Token> CombatPool { get; private set; }
        public List<Token> SpentPool { get; private set; }

        public event Action OnSpentPoolUpdated;
        public event Action OnCombatPoolUpdated;
        
        public TokenPool(List<Token> tokens)
        {
            All = tokens;
            CombatPool = tokens;
            SpentPool = new List<Token>();
        }

        public void Cast()
        {
            All.ForEach(token => token.Cast());
        }

        public void Spend(List<Token> spentTokens)
        {
            foreach (var token in spentTokens.Where(token => CombatPool.Contains(token)))
            {
                CombatPool.Remove(token);
                SpentPool.Add(token);
            }
            
            OnCombatPoolUpdated?.Invoke();
            OnSpentPoolUpdated?.Invoke();
        }

        public bool IsSpent(Token token)
        {
            return SpentPool.Contains(token);
        }
    }
}