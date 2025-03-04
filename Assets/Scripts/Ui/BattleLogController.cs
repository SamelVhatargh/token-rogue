using UnityEngine;
using UnityEngine.UIElements;

namespace Ui
{
    public class BattleLogController : MonoBehaviour
    {
        [SerializeField] private UIDocument document;

        private ScrollView _battleLog;
        private Battle.Battle _battle;

        private void Awake()
        {
            _battleLog = document.rootVisualElement.Q<ScrollView>("BattleLog");
        }
        
        public void SetBattle(Battle.Battle battle)
        {
            _battle = battle;
            _battle.OnCombatActionExecuted += action => AddLogMessage(action.GetLogMessage());
            _battle.OnRoundStarted += (round, combatant)
                => AddLogMessage($"Round {round} started. {combatant} won the initiative.");
        }

        private void AddLogMessage(string getLogMessage)
        {
            var logEntry = new Label(getLogMessage);
            _battleLog.contentContainer.Add(logEntry);
            Debug.Log(getLogMessage);
        }
    }
}