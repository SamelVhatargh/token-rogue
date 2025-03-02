using Battle.Combatants;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ui
{
    public class BattleScreenView : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        
        private Label _playerHealthLabel;
        private Label _opponentHealthLabel;

        private void Awake()
        {
            _playerHealthLabel = document.rootVisualElement.Query<Label>("HealthLabel").First();
            _opponentHealthLabel = document.rootVisualElement.Query<Label>("HealthLabel").Last();
        }
        
        public void SetCombatantStats(Stats playerStats, Stats opponentStats)
        {
            UpdateHealthLabel(_playerHealthLabel, playerStats);
            UpdateHealthLabel(_opponentHealthLabel, opponentStats);
        }
        
        private void UpdateHealthLabel(Label label, Stats stats)
        {
            label.text = $"Health: {stats.Health} / {stats.MaxHealth}";
        }
    }
}