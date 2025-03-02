using System;
using System.Collections.Generic;
using Battle.CombatActions;
using Battle.Tokens;
using UnityEngine;

namespace Battle.Combatants
{
    public class Player : MonoBehaviour, ICombatant
    {
        [SerializeField] private TokenPoolView tokenPoolView;
        
        public event Action<ICombatAction> OnActionTaken;
        
        public TokenPool Tokens { get; private set; }

        private void Awake()
        {
            Tokens = new TokenPool(new List<Token>
            {
                new(new Side(2, Symbol.Attack), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Energy), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Attack), new Side(0, Symbol.None)),
                new(new Side(1, Symbol.Agility), new Side(0, Symbol.None)),
            });
            tokenPoolView.Render(Tokens);
        }

        private bool _isPlayerTurn;

        private void Update()
        {
            if (!_isPlayerTurn || !Input.GetKeyDown(KeyCode.Space))
            {
                return;
            }

            var action = new TestCombatAction();
            OnActionTaken?.Invoke(action);
            _isPlayerTurn = false;
        }

        public void DoCombatAction()
        {
            _isPlayerTurn = true;
        }

        public override string ToString()
        {
            return "Player";
        }
    }
}