using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Tokens
{
    public class TokenPool
    {
        private List<Token> All => CombatPool.Concat(SpentPool).ToList();
        public List<Token> CombatPool { get; private set; }
        public List<Token> SpentPool { get; private set; }

        public event Action OnSpentPoolUpdated;
        public event Action OnCombatPoolUpdated;
        
        public TokenPool(List<Token> tokens)
        {
            CombatPool = tokens;
            SpentPool = new List<Token>();
        }

        public void Cast()
        {
            CombatPool.AddRange(SpentPool);
            SpentPool.Clear();
        
            OnCombatPoolUpdated?.Invoke();
            OnSpentPoolUpdated?.Invoke();
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
        
        public string PrintActiveTokenCombatPool()
        {
            return string.Join(", ", CombatPool.Select(token => token.ActiveSide));
        }
    }
}