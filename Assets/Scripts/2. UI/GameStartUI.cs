using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    PlayerController controller;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller.GetComponent<PlayerController>();
    }

    public void OnClickStartButton()
    {
        UIManager.Instance.SetRule();
        //controller.ToggleCursor(false);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameStart;
    }
}
