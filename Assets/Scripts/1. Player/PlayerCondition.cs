using System;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(float damage);
}


public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    private PlayerController controller;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (health.curValue <= 0f)
        {
            Die();
        }
    }


    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        UIManager.Instance.SetGameOver();
    }

    public void SpeedUpDown(float amount)
    {
        controller.moveSpeed += amount;
    }

    public void TakePhysicalDamage(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}