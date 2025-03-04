using System.Collections.Generic;
using Battle.Combatants;
using Battle.Tokens;
using Ui;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private AiCombatant ai;
        [SerializeField] private HealthDisplayController healthDisplayController;
        [SerializeField] private BattleLogController battleLogController;

        private Battle _battle;

        private void Awake()
        {
            player.SetStats(new Stats("Player", 8, new TokenPool(new List<Token>
            {
                // new(new Side(1, Symbol.Attack), new Side(1, Symbol.Attack)),
                // new(new Side(1, Symbol.Attack), new Side(1, Symbol.Attack)),

                new(new Side(2, Symbol.Attack), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Energy), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Attack), new Side(0, Symbol.None)),
                new(new Side(1, Symbol.Agility), new Side(0, Symbol.None)),
            })));
            ai.SetStats(new Stats("Monster", 5, new TokenPool(new List<Token>
            {
                new(new Side(2, Symbol.Attack), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Energy), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Attack), new Side(0, Symbol.None)),
                new(new Side(1, Symbol.Agility, true), new Side(0, Symbol.None)),
            })));
            player.SetOpponent(ai);
            ai.SetOpponent(player);
            
            healthDisplayController.SetCombatantStats(player.Stats, ai.Stats);
            
            _battle = new Battle(player, ai);
            battleLogController.SetBattle(_battle);
            _battle.Start();
        }
    }
}