using System.Collections.Generic;
using Battle.Combatants;
using Battle.Tokens;

namespace Battle.CombatActions
{
    public class AttackAction : ICombatAction
    {
        private readonly Stats _defender;
        private readonly Stats _attacker;
        private readonly int _damage;
        private readonly List<Token> _spentTokens;

        public AttackAction(Stats attacker, Stats defender, int damage, List<Token> tokens)
        {
            _attacker = attacker;
            _defender = defender;
            _damage = damage;
            _spentTokens = tokens;
        }
        
        public void Execute()
        {
            _defender.TakeDamage(_damage);
            _attacker.Tokens.Spend(_spentTokens);
        }

        public string GetLogMessage()
        {
            return $"{_attacker.Name} deals {_damage} damage to {_defender.Name}";
        }
    }
}