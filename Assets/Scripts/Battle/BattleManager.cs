using Battle.Combatants;
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
            _battle = new Battle(player, ai);
            _battle.Start();
        }
    }
}