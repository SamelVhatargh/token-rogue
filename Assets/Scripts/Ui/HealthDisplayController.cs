using Battle.Combatants;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ui
{
    public class HealthDisplayController : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        
        private Label _playerHealthLabel;
        private Label _opponentHealthLabel;
        
        private Stats _playerStats;
        private Stats _opponentStats;

        private void Awake()
        {
            _playerHealthLabel = document.rootVisualElement.Query<Label>("HealthLabel").First();
            _opponentHealthLabel = document.rootVisualElement.Query<Label>("HealthLabel").Last();
        }
        
        public void SetCombatantStats(Stats playerStats, Stats opponentStats)
        {
            _playerStats = playerStats;
            _opponentStats = opponentStats;
            
            _playerStats.OnHealthChanged += () => UpdateHealthLabel(_playerHealthLabel, _playerStats);
            _opponentStats.OnHealthChanged += () => UpdateHealthLabel(_opponentHealthLabel, _opponentStats);
            
            UpdateHealthLabel(_playerHealthLabel, playerStats);
            UpdateHealthLabel(_opponentHealthLabel, opponentStats);
        }
        
        private void UpdateHealthLabel(Label label, Stats stats)
        {
            label.text = $"Health: {stats.Health} / {stats.MaxHealth}";
        }
    }
}