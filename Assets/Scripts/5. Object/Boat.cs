using UnityEngine;

public class Boat : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Player"))
       {
            UIManager.Instance.SetClear();
       }
    }
}
