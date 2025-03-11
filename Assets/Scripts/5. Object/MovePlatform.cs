using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Vector3 targetPosition;
    public float maxDistance;
    private bool isForward;
    private bool isBackward;

    void Start()
    {
        ForwardMove();
    }

    void Update()
    {
        if (transform.position == targetPosition && isBackward)
        {
            ForwardMove();
        }
        else if (transform.position == targetPosition && isForward)
        {
            BackwardMove();
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);
    }

    void ForwardMove()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + maxDistance);
        isForward = true;
        isBackward = false;
    }

    void BackwardMove()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - maxDistance);
        isForward = false;
        isBackward = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}