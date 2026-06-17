using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Move Settings")]
    public float speed = 5f;
    public float destroyZ = -6f;
    public bool canMove = true;

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        transform.position += Vector3.back * speed * Time.deltaTime;

        if (transform.position.z <= destroyZ)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetMoveEnabled(bool enabled)
    {
        canMove = enabled;
    }
}
