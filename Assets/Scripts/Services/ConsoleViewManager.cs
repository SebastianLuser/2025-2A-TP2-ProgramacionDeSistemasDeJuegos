﻿using DebugConsole;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Services
{
    public class ConsoleViewManager : MonoBehaviour
    {
        private const int CHARACTER_LIMIT = 13000;

        [SerializeField] private TMP_Text consoleBody;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button submit;
        [SerializeField] private Button toggleButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private ConsoleWrapper consoleWrapper;
        [SerializeField] private InputActionReference toggleConsoleAction;
        [SerializeField] private GameObject consolePanel;

        public bool IsConsoleOpen => consolePanel != null && consolePanel.activeSelf;

        private void Awake()
        {
            ServiceLocator.Register(this);
        }

        private void Start()
        {
            if (consolePanel != null)
                consolePanel.SetActive(false);
            
            if (consoleWrapper)
            {

                var commandProvider = ServiceLocator.Get<CommandProvider>();
                var externalCommands = commandProvider.CreateCommands(consoleWrapper);
                foreach (var command in externalCommands)
                {
                    consoleWrapper.AddCommand(command);
                }

            }
        }

        private void OnEnable()
        {
            submit?.onClick.AddListener(HandleSubmitClick);
            toggleButton?.onClick.AddListener(ToggleConsole);
            closeButton?.onClick.AddListener(CloseConsole);
            inputField?.onSubmit.AddListener(SubmitInput);
            
            if (consoleWrapper)
                consoleWrapper.log += WriteToOutput;
                
            if (toggleConsoleAction != null)
            {
                toggleConsoleAction.action.Enable();
                toggleConsoleAction.action.performed += OnToggleAction;
            }
        }

        private void OnDisable()
        {
            submit?.onClick.RemoveListener(HandleSubmitClick);
            toggleButton?.onClick.RemoveListener(ToggleConsole);
            closeButton?.onClick.RemoveListener(CloseConsole);
            inputField?.onSubmit.RemoveListener(SubmitInput);
            
            if (consoleWrapper)
                consoleWrapper.log -= WriteToOutput;
        }
        
        private void OnDestroy()
        {
            if (toggleConsoleAction != null)
            {
                toggleConsoleAction.action.performed -= OnToggleAction;
                toggleConsoleAction.action.Disable();
            }
            
            ServiceLocator.Unregister<ConsoleViewManager>();
        }

        private void HandleSubmitClick() => SubmitInput(inputField.text);

        private void SubmitInput(string input)
        {
            if (input == string.Empty)
                return;

            if (consoleWrapper == null)
            {
                return;
            }

            _ = consoleWrapper.TryUseInput(input);
            inputField?.SetTextWithoutNotify(string.Empty);
        }

        public void WriteToOutput(string newFeedback)
        {
            if (!consoleBody)
            {
                return;
            }

            consoleBody.text += "\n" + newFeedback;
            var watchdog = 10;
            var bodyText = consoleBody.text;

            while (watchdog-- > 0 && bodyText.Length >= CHARACTER_LIMIT)
            {
                var newBody = bodyText[(bodyText.IndexOf('\n') + 1)..];
                consoleBody.text = newBody;
                bodyText = newBody;
            }

            if (IsConsoleOpen && inputField != null)
            {
                inputField.ActivateInputField();
            }
        }
        
        private void OnToggleAction(InputAction.CallbackContext ctx)
        {
            ToggleConsole();
        }
        
        public void ToggleConsole()
        {
            if (consolePanel != null)
            {
                bool isActive = !consolePanel.activeSelf;
                consolePanel.SetActive(isActive);
                
                if (isActive && inputField != null)
                {
                    inputField.ActivateInputField();
                }
            }
        }
        
        public void CloseConsole()
        {
            if (consolePanel != null)
            {
                consolePanel.SetActive(false);
            }
        }
    }
}