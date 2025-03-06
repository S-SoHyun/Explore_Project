using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    //Condition health { get { return uiCondition.health; } }


    void Start()
    {
        
    }

    void Update()
    {
        //if (health.curValue <= 0f)
        //{
        //    Die();
        //}
    }

    public void Die()
    {
        // GameOverUI 생성 후 restart할 수 있게.
    }
}
