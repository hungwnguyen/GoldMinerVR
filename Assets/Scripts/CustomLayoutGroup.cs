using System.Collections;
using UnityEngine;

public class CustomLayoutGroup : MonoBehaviour
{
    [Tooltip("spacing of gameObject child")]
    [SerializeField] private float spacing = 2.5f;
    private float speedScale = 0.02f;
    private float timeToScale;
    int NumberPlayer = 0;

    void Start()
    {
        timeToScale = 0;
        this.transform.localScale = Vector3.one * 0.8f;
    }

    void Update()
    {
        UpdateLayout();
        if (timeToScale >= 0 && GameManager.Instance.ShowCountdown && GameManager.Instance.PlayMode == GameManager.eGameState.START)
        {
            UpdateScale();
        }
    }

    public void UpdateScale()
    {
        timeToScale += Time.deltaTime;
        if (timeToScale < 0.55f)
        {
            this.transform.localScale -= Time.deltaTime * speedScale * Vector3.one;
        }
        else if (timeToScale < 1)
        {
            this.transform.localScale += Time.deltaTime * speedScale * Vector3.one * 16 / 9;
        }
        else
        {
            this.transform.localScale = Vector3.one;
            timeToScale = -1;
            Destroy(this.GetComponent<CustomLayoutGroup>());
        }

    }

    void UpdateLayout()
    {
        NumberPlayer = this.transform.childCount;
        int halfCount = NumberPlayer / 2;

        for (int i = 0; i < NumberPlayer; i++)
        {
            Transform child = transform.GetChild(i);
            /*if (child.gameObject.CompareTag("Player"))
            {*/
            float xPosition;

            if (NumberPlayer % 2 == 0)
            {
                xPosition = (i - halfCount + 0.5f) * spacing;
            }
            else
            {
                xPosition = (i - halfCount) * spacing;
            }

            child.position = new Vector3(xPosition, child.position.y, child.position.z);
            //}
        }
    }
}
