using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        UIManager.Instance.SetRule();
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
