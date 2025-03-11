using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class RuleUI : BaseUI
{
    [SerializeField] private Button ruleButton;

    PlayerController controller;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        ruleButton.onClick.AddListener(OnClickRuleButton);
    }

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller.GetComponent<PlayerController>();
    }

    public void OnClickRuleButton()
    {
        ruleButton.gameObject.SetActive(false);
        controller.ToggleCursor(false);
    }

    protected override UIState GetUIState()
    {
        return UIState.Rule;
    }
}
