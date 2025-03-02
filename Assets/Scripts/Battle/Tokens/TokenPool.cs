using System.Collections.Generic;

namespace Battle.Tokens
{
    public class TokenPool
    {
        public List<Token> All { get; private set; }
        public List<Token> CombatPool { get; private set; }
        public List<Token> SpentPool { get; private set; }

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
    }
}