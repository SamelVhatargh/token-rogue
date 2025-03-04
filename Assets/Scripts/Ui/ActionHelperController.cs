using System;
using UnityEngine;

namespace Ui
{
    public class ActionHelperController : MonoBehaviour
    {
        public event Action OnConfirmClicked;
        public event Action OnCancelClicked;

        public void Init(string message)
        {
            SetMessage(message);
            EnableCancelButton();
        }

        private void Update()
        {
            if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("Confirm");
                OnConfirmClicked?.Invoke();
            }
        
            if (UnityEngine.InputSystem.Keyboard.current.cKey.wasPressedThisFrame)
            {
                OnCancelClicked?.Invoke();
            }
        }

        public void SetMessage(string message)
        {
            Debug.Log(message);
        }
        
        public void EnableConfirmButton()
        {
            
        }
        
        public void DisableConfirmButton()
        {
            
        }

        public void EnableCancelButton()
        {
            
        }
        
        public void DisableCancelButton()
        {
            
        }
    }
}