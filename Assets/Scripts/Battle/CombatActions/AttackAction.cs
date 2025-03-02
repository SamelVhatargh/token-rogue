using Battle.Combatants;

namespace Battle.CombatActions
{
    public class AttackAction : ICombatAction
    {
        private readonly Stats _defender;
        private readonly int _damage;

        public AttackAction(Stats defender, int damage)
        {
            _defender = defender;
            _damage = damage;
        }
        
        public void Execute()
        {
            _defender.TakeDamage(_damage);
        }
    }
}