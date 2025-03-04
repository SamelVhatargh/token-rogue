using System.Collections.Generic;
using Battle.Combatants;
using Battle.Tokens;

namespace Battle.CombatActions
{
    public class RecastAction : ICombatAction
    {
        private readonly Token _recastToken;
        private readonly Stats _executor;
        private readonly Stats _target;
        private readonly List<Token> _usedTokens;
        private Side _oldSide;

        public RecastAction(Token recastToken, Stats executor, Stats target, List<Token> usedTokens)
        {
            _recastToken = recastToken;
            _executor = executor;
            _target = target;
            _usedTokens = usedTokens;
        }

        public void Execute()
        {
            _oldSide = _recastToken.ActiveSide;
            _recastToken.Cast();
            _executor.Tokens.Spend(_usedTokens);
        }

        public string GetLogMessage()
        {
            return $"{_executor.Name} recast {_target.Name} {_oldSide.Symbol.ToString().ToLower()}"
                   + $" token to {_recastToken.ActiveSide.Symbol.ToString().ToLower()}";
        }
    }
}