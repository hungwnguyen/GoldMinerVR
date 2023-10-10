using UnityEngine;

public class MoveUntilNoCollision2D : MonoBehaviour
{
    public float moveSpeed = 1f;
    private PolygonCollider2D polygonCollider2D;

    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (gameObject.CompareTag("mouse"))
        {
            return;
        }

        Vector2 newPosition = transform.position + Vector3.left * moveSpeed * Time.deltaTime;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(newPosition, polygonCollider2D.bounds.size, 0);

        bool hasCollision = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                hasCollision = true;
                break;
            }
        }

        if (!hasCollision)
        {
            transform.position = newPosition;
        }
    }
}
