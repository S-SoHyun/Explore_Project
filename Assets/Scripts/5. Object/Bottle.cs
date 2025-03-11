using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomEffect   // ���� ȿ�� ����
{
    private int number;     // �������� �� �� ����� ��ȣ ����
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
        new RandomEffect(0, 1000f, 0f),   // ���
        new RandomEffect(1, 20f, 0f),      // ü�� +20
        new RandomEffect(2, 20f, 0f),     // ü�� -20
        new RandomEffect(3, 0f, 3f),       // ���ǵ� +3
        new RandomEffect(4, 0f, 4f),      // ���ǵ� -4
    };

    private int randomSelect;

    [SerializeField] private TextMeshProUGUI effectText;

    private PlayerCondition condition;


    void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
        effectText = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    public void InputActive()                           // input ������
    {
        RandomEffect selectedEffect = Gacha();          // ���� ȿ�� �̱�
        ActEffect(selectedEffect);                      // �̾����� ����
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
            case 0: // ü�� -1000
                condition.Deal(selectedEffect.Hp);
                Destroy(gameObject);
                Debug.Log("���");
                break;
            case 1: // ü�� +20
                condition.Heal(selectedEffect.Hp);
                Destroy(gameObject);
                Debug.Log("+20");
                break;
            case 2: // ü�� -20
                condition.Deal(selectedEffect.Hp);
                Destroy(gameObject);
                Debug.Log("-20");
                break;
            case 3: // ���ǵ� +3
                StartCoroutine(condition.SpeedUp(selectedEffect.Speed));
                Debug.Log("���ǵ�+3");
                break;
            case 4: // ���ǵ� -4
                StartCoroutine(condition.SpeedDown(selectedEffect.Speed));
                Debug.Log("���ǵ�-4");
                break;
        }
        StartCoroutine(ShowEffectText(selectedEffect));
    }

    private IEnumerator ShowEffectText(RandomEffect selectedEffect)    // �ɸ� ȿ���� text�� ��� �����ֱ�
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