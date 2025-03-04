using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Tokens
{
    public class TokenPool
    {
        public List<Token> All { get; }
        public List<Token> CombatPool => All.Where(token => !token.IsSpent).ToList();
        public List<Token> SpentPool => All.Where(token => token.IsSpent).ToList();

        public event Action OnPoolUpdated;
        
        public TokenPool(List<Token> tokens)
        {
            All = tokens;
        }

        public void Cast()
        {
            All.ForEach(token => token.Cast());
            OnPoolUpdated?.Invoke();
        }

        public void Spend(List<Token> spentTokens)
        {
            foreach (var token in spentTokens.Where(token => CombatPool.Contains(token)))
            {
                token.Spend();
            }
            
            OnPoolUpdated?.Invoke();
        }
        
        public int GetInitiative()
        {
            return CombatPool.Count(token => token.ActiveSide.HasInitiative);
        }
        
        public string PrintActiveTokenCombatPool()
        {
            return string.Join(", ", CombatPool.Select(token => token.ActiveSide));
        }
    }
}