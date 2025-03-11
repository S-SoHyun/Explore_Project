using System.Collections;
using System.Collections.Generic;
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
        ChangeState(UIState.GameStart);
    }

    public void SetRule()
    {
        ChangeState(UIState.Rule);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void SetClear()
    {
        ChangeState(UIState.Clear);
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
