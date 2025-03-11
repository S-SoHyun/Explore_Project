using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomEffect   // 랜덤 효과 생성
{
    private int number;     // 랜덤으로 고를 때 사용할 번호 추출
    private float hp;
    private float speed;

    public int Number { get; set; }
    public float Hp { get; set; }
    public float Speed { get; set; }

    public RandomEffect(int number, float hp, float speed)
    {
        Number = number;
        Hp = hp;
        Speed = speed;
    }
}


public class Bottle : MonoBehaviour
{
    private List<RandomEffect> randomEffects = new List<RandomEffect>
    {
        // number, hp, speed
        new RandomEffect(0, 1000f, 0f),   // 즉사
        new RandomEffect(1, 20f, 0f),      // 체력 +20
        new RandomEffect(2, 20f, 0f),     // 체력 -20
        new RandomEffect(3, 0f, 3f),       // 스피드 +3
        new RandomEffect(4, 0f, 4f),      // 스피드 -4
    };

    private int randomSelect;

    [SerializeField] private TextMeshProUGUI effectText;

    private PlayerCondition condition;


    void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
        effectText = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    public void InputActive()                           // input 들어오면
    {
        RandomEffect selectedEffect = Gacha();          // 랜덤 효과 뽑기
        ActEffect(selectedEffect);                      // 뽑았으면 실행
    }

    private RandomEffect Gacha()
    {
        randomSelect = Random.Range(0, randomEffects.Count);
        return randomEffects[randomSelect];
    }

    private void ActEffect(RandomEffect selectedEffect)
    {
        switch (randomSelect)
        {
            case 0: // 체력 -1000
                condition.Deal(selectedEffect.Hp);
                Destroy(gameObject);
                Debug.Log("즉사");
                break;
            case 1: // 체력 +20
                condition.Heal(selectedEffect.Hp);
                Destroy(gameObject);
                Debug.Log("+20");
                break;
            case 2: // 체력 -20
                condition.Deal(selectedEffect.Hp);
                Destroy(gameObject);
                Debug.Log("-20");
                break;
            case 3: // 스피드 +3
                StartCoroutine(condition.SpeedUp(selectedEffect.Speed));
                Debug.Log("스피드+3");
                break;
            case 4: // 스피드 -4
                StartCoroutine(condition.SpeedDown(selectedEffect.Speed));
                Debug.Log("스피드-4");
                break;
        }
        StartCoroutine(ShowEffectText(selectedEffect));
    }

    private IEnumerator ShowEffectText(RandomEffect selectedEffect)    // 걸린 효과를 text로 잠깐 보여주기
    {
        string statName = selectedEffect.Hp != 0 ? "HP " : "Speed ";
        float statValue = selectedEffect.Hp != 0 ? selectedEffect.Hp : selectedEffect.Speed;
        string plusMinus = PlusMinus(selectedEffect);

        effectText.text = statName + plusMinus + statValue;

        effectText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        effectText.gameObject.SetActive(false);
    }

    private string PlusMinus(RandomEffect selectedEffect)
    {
        if (selectedEffect.Number % 2 == 0)
            return "-";
        else
            return "+";
    }
}