using System.Collections.Generic;
using Battle.Combatants;
using Battle.Tokens;
using UnityEngine;

namespace Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private AiCombatant ai;

        private Battle _battle;

        private void Awake()
        {
            player.SetStats(new Stats(8, new TokenPool(new List<Token>
            {
                new(new Side(2, Symbol.Attack), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Energy), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Attack), new Side(0, Symbol.None)),
                new(new Side(1, Symbol.Agility), new Side(0, Symbol.None)),
            })));
            ai.SetStats(new Stats(5, new TokenPool(new List<Token>
            {
                new(new Side(2, Symbol.Attack), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Energy), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Attack), new Side(0, Symbol.None)),
                new(new Side(1, Symbol.Agility), new Side(0, Symbol.None)),
            })));
            _battle = new Battle(player, ai);
            _battle.Start();
        }
    }
}