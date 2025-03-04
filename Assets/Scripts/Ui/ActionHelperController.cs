using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ui
{
    public class ActionHelperController : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        
        public event Action OnConfirmClicked;
        public event Action OnCancelClicked;
        
        private Label _messageLabel;
        private Button _confirmButton;
        private Button _cancelButton;

        private void Awake()
        {
            _messageLabel = document.rootVisualElement.Q<Label>("ActionHelperMessage");
            _confirmButton = document.rootVisualElement.Q<Button>("ConfirmButton");
            _cancelButton = document.rootVisualElement.Q<Button>("CancelButton");
            
            _confirmButton.clicked += () => OnConfirmClicked?.Invoke();
            _cancelButton.clicked += () => OnCancelClicked?.Invoke();
        }

        public void Init(string message)
        {
            SetMessage(message);
            EnableCancelButton();
        }

        public void SetMessage(string message)
        {
            _messageLabel.text = message;
        }
        
        public void EnableConfirmButton()
        {
            _confirmButton.SetEnabled(true);
        }
        
        public void DisableConfirmButton()
        {
            _confirmButton.SetEnabled(false);
        }

        public void EnableCancelButton()
        {
            _cancelButton.SetEnabled(true);
        }
        
        public void DisableCancelButton()
        {
            _cancelButton.SetEnabled(false);
        }
    }
}