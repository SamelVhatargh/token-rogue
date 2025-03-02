using Battle.Tokens;

namespace Battle.Combatants
{
    public class Stats
    {
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public TokenPool Tokens { get; private set; }
        
        public Stats(int health, TokenPool tokens)
        {
            MaxHealth = health;
            Health = health;
            Tokens = tokens;
        }
        
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}