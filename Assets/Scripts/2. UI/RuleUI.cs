using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleUI : BaseUI
{
    [SerializeField] private Button ruleButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        ruleButton.onClick.AddListener(OnClickRuleButton);
    }

    public void OnClickRuleButton()
    {
        ruleButton.gameObject.SetActive(false);
    }

    protected override UIState GetUIState()
    {
        return UIState.Rule;
    }
}
