using System;
using Battle.Tokens;
using UnityEngine;

namespace Battle.Combatants
{
    public class Stats
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public TokenPool Tokens { get; private set; }
        public event Action OnHealthChanged;
        
        public Stats(string name, int health, TokenPool tokens)
        {
            Name = name;
            MaxHealth = health;
            Health = health;
            Tokens = tokens;
        }
        
        public void TakeDamage(int damage)
        {
            Health -= damage;
            OnHealthChanged?.Invoke();
        }
    }
}