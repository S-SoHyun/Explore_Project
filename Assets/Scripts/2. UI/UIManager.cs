using UnityEngine;

public enum UIState
{
    GameStart,
    Rule,
    GameOver,
    Clear
}


public class UIManager : Singleton<UIManager>
{
    GameStartUI gameStartUI;
    RuleUI ruleUI;
    GameOverUI gameOverUI;
    ClearUI clearUI;
    UIState curState;

    PlayerController controller;

    private void Start()
    {
        gameStartUI = GetComponentInChildren<GameStartUI>(true);
        gameStartUI.Init(this);
        ruleUI = GetComponentInChildren<RuleUI>(true);
        ruleUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);
        clearUI = GetComponentInChildren<ClearUI>(true);
        clearUI.Init(this);

        controller = CharacterManager.Instance.Player.controller.GetComponent<PlayerController>();

        SetGameStart();
    }

    public void SetGameStart()
    {
        ChangeState(UIState.GameStart);
        controller.ToggleCursor(true);
    }

    public void SetRule()
    {
        ChangeState(UIState.Rule);
        controller.ToggleCursor(true);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
        controller.ToggleCursor(true);
    }

    public void SetClear()
    {
        ChangeState(UIState.Clear);
        controller.ToggleCursor(true);
    }

    public void ChangeState(UIState state)
    {
        curState = state;
        gameStartUI.SetActive(curState);
        ruleUI.SetActive(curState);
        gameOverUI.SetActive(curState);
        clearUI.SetActive(curState);
    }
}
