using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace SG
{

public class RebindingDisplay : MonoBehaviour
{
        [SerializeField] private InputActionReference jump = null;
        [SerializeField] private PlayerInput playerInput = null;
        [SerializeField] private TMP_Text bindingDisplayNameText = null;
        [SerializeField] private GameObject startRebindObject = null;
        [SerializeField] private GameObject waitingForInputObject = null;

        private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

        private const string RebindsKey = "rebinds";
        private PlayerInput PlayerInput => playerInput;

        private void Start()
        {
            string rebinds = PlayerPrefs.GetString(RebindsKey, string.Empty);

            if(string.IsNullOrEmpty(rebinds)) { return; }

            playerInput.actions.LoadBindingOverridesFromJson(rebinds);

            int bindingIndex = jump.action.GetBindingIndexForControl(jump.action.controls[0]);

            bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(jump.action.bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        public void SaveBindings()
        {
            string rebinds = playerInput.actions.SaveBindingOverridesAsJson();

            PlayerPrefs.SetString(RebindsKey, rebinds);
        }

        public void StartRebinding()
        {
            startRebindObject.SetActive(false);
            waitingForInputObject.SetActive(true);

            playerInput.SwitchCurrentActionMap("Menu");

            rebindingOperation = jump.action.PerformInteractiveRebinding()
                    .WithControlsExcluding("PlayerActions")
                    .OnMatchWaitForAnother(0.1f)
                    .OnComplete(operation => RebindComplete())
                    .Start();
        }

        private void RebindComplete()
        {
            int bindingIndex = jump.action.GetBindingIndexForControl(jump.action.controls[0]);

            bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(jump.action.bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            rebindingOperation.Dispose();

            startRebindObject.SetActive(true);
            waitingForInputObject.SetActive(false);
        }
}
}