using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    [SerializeField]private float distance = 5f;
    private float speed = 3f;

    private float initialPositionX;
    private float targetPositionX;
    private bool movingLeft;

    private void Start()
    {
        initialPositionX = transform.position.x;
        targetPositionX = initialPositionX - distance;
        movingLeft = true;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        float newPositionX = Mathf.MoveTowards(transform.position.x, targetPositionX, step);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

        if (Mathf.Abs(newPositionX - targetPositionX) < 0.01f)
        {
            if (movingLeft)
            {
                targetPositionX = initialPositionX + distance;
                RotateAroundYAxis();
            }
            else
            {
                targetPositionX = initialPositionX - distance;
                RotateAroundYAxis();
            }
            movingLeft = !movingLeft;
        }
    }

    private void RotateAroundYAxis()
    {
        transform.Rotate(0, 180, 0);
    }
}
