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
        public Stats Stats { get; private set; }

        private void Awake()
        {
            var tokens = new TokenPool(new List<Token>
            {
                new(new Side(2, Symbol.Attack), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Energy), new Side(1, Symbol.Defense)),
                new(new Side(1, Symbol.Attack), new Side(0, Symbol.None)),
                new(new Side(1, Symbol.Agility), new Side(0, Symbol.None)),
            });
            Stats = new Stats(8, tokens);
            tokenPoolView.Render(tokens);
        }

        private bool _isPlayerTurn;


        private void Update()
        {
            if (!_isPlayerTurn)
            {
                return;
            }
            
            // if (Input.GetKeyDown(KeyCode.C))
            // {
            //     Tokens.Cast();
            //     return;
            // }
            //
            // if (!Input.GetKeyDown(KeyCode.Space))
            // {
            //     return;
            // }
            return;

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