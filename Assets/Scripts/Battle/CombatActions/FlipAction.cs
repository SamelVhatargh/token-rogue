using System.Collections.Generic;
using Battle.Combatants;
using Battle.Tokens;

namespace Battle.CombatActions
{
    public class FlipAction : ICombatAction
    {
        private readonly Token _flippedToken;
        private readonly Stats _executor;
        private readonly Stats _target;
        private readonly List<Token> _usedTokens;

        public FlipAction(Token flippedToken, Stats executor, Stats target, List<Token> usedTokens)
        {
            _flippedToken = flippedToken;
            _executor = executor;
            _target = target;
            _usedTokens = usedTokens;
        }

        public void Execute()
        {
            _flippedToken.Flip();
            _executor.Tokens.Spend(_usedTokens);
        }

        public string GetLogMessage()
        {
            return $"{_executor.Name} flips {_target.Name} {_flippedToken.InactiveSide.Symbol.ToString().ToLower()}"
                   + $" to {_flippedToken.ActiveSide.Symbol.ToString().ToLower()}";
        }
    }
}