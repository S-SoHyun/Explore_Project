using System;
using System.Collections;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(float damage);
}


public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition speed { get { return uiCondition.speed; } }

    public event Action onTakeDamage;

    void Update()
    {
        if (health.curValue <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Deal(float amount)
    {
        health.Subtract(amount);
    }

    public IEnumerator SpeedUp(float amount)
    {
        speed.Add(amount);
        yield return new WaitForSeconds(5f);
        speed.Subtract(amount);
    }

    public IEnumerator SpeedDown(float amount)
    {
        speed.Subtract(amount);
        yield return new WaitForSeconds(5f);
        speed.Add(amount);
    }

    public void TakePhysicalDamage(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public void Die()
    {
        UIManager.Instance.SetGameOver();
    }
}
