using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    GameStart,
    Rule,
    GameOver
}


public class UIManager : Singleton<UIManager>
{
    GameStartUI gameStartUI;
    RuleUI ruleUI;
    GameOverUI gameOverUI;
    private UIState curState;

    private void Start()
    {
        gameStartUI = GetComponentInChildren<GameStartUI>(true);
        gameStartUI.Init(this);
        ruleUI = GetComponentInChildren<RuleUI>(true);
        ruleUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);

        //ChangeState(UIState.GameStart);
        SetGameStart();
    }

    public void SetGameStart()
    {
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

    public void ChangeState(UIState state)
    {
        curState = state;
        gameStartUI.SetActive(curState);
        ruleUI.SetActive(curState);
        gameOverUI.SetActive(curState);
    }
}
