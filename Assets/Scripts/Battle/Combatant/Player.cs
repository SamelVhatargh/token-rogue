﻿using System;
using Battle.CombatAction;
using UnityEngine;

namespace Battle.Combatant
{
    public class Player : MonoBehaviour, ICombatant
    {
        public event Action<ICombatAction> OnActionTaken;

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