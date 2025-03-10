using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomEffect
{
    private int number;    // ���� ��ȣ ������ ����
    private float hp;
    private float speed;
    private float speedTime;

    public int Number { get; set; }
    public float Hp { get; set; }
    public float Speed { get; set; }
    public float SpeedTime { get; set; }

    public RandomEffect(int number, float hp, float speed, float speedTime)
    {
        Number = number;
        Hp = hp;
        Speed = speed;
        SpeedTime = speedTime;
    }
}


public class Bottle : MonoBehaviour
{
    private List<RandomEffect> randomEffects = new List<RandomEffect>
    {
        new RandomEffect(0, -1000f, 0f, 0f),   // ���
        new RandomEffect(1, 20f, 0f, 0f),     // ü�� +20
        new RandomEffect(2, -20f, 0f, 0f),    // ü�� -20
        new RandomEffect(3, 0f, 3f, 3f),     // ���ǵ� +3
        new RandomEffect(4, 0f, -4f, 5f),   // ���ǵ� -4
    };

    private int randomSelect;

    public TextMeshProUGUI effectText;

    private PlayerCondition condition;


    void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
        effectText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

    }

    public void OnUseInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)      // ���� ���� ��ǲ�� ������
        {
            // ����Ʈ���� �ϳ� �̱�
            RandomEffect selectedEffect = Gacha();

            // ���� �� ����
            ActEffect(selectedEffect);
        }
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
            case 0: // ���
                condition.Heal(selectedEffect.Hp);
                Debug.Log("���");
                break;
            case 1: // ü�� +20
                condition.Heal(selectedEffect.Hp);
                Debug.Log("+20");
                break;
            case 2: // ü�� -20
                condition.Heal(selectedEffect.Hp);
                Debug.Log("-20");
                break;
            case 3: // ���ǵ� +3
                StartCoroutine(ApplySpeed(selectedEffect));
                Debug.Log("���ǵ�+3");
                break;
            case 4: // ���ǵ� -4
                StartCoroutine(ApplySpeed(selectedEffect));
                Debug.Log("���ǵ�-4");
                break;
        }
        //StartCoroutine(ShowEffectText(selectedEffect));
    }


    private IEnumerator ApplySpeed(RandomEffect selectedEffect)
    {
        float speed = selectedEffect.Speed;
        condition.SpeedUpDown(speed);
        yield return new WaitForSeconds(5f);
        condition.SpeedUpDown(-speed);
    }

    //private IEnumerator ShowEffectText(RandomEffect selectedEffect)    // �ش� ȿ���� text�� ��� �����ְ� ������� �ϱ�
    //{
    //    effectText = transform.parent.find


    //    string statName = selectedEffect.Hp != 0 ? "HP " : "Speed ";
    //    float statValue = selectedEffect.Hp != 0 ? selectedEffect.Hp : selectedEffect.Speed;
    //    effectText.text = statName + statValue;

    //    effectText.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(3f);


    //    effectText.gameObject.SetActive(false);
    //}
}
