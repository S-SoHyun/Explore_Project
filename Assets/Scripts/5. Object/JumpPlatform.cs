using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float jumpPlatformPower;
    private Rigidbody rb;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.TryGetComponent<Rigidbody>(out rb))
        {
            rb.AddForce(Vector2.up * jumpPlatformPower, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("No Rigidbody");
        }
    }
}
