using UnityEngine;

public class CustomLayoutGroup : MonoBehaviour
{
    [Tooltip("spacing of gameObject child")]
    [SerializeField] private float spacing = 2.5f;
    [SerializeField] private float speedScale = 1f;
    private float timeToScale;

    void Start()
    {
        timeToScale = 0;
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
        if (timeToScale < 0.75f)
        {
            this.transform.localScale -= Time.deltaTime * speedScale * Vector3.one;
        }
        else if (timeToScale < 1)
        {
            this.transform.localScale += Time.deltaTime * speedScale * Vector3.one * 3;
        }
        else
        {
            this.transform.localScale = Vector3.one;
            timeToScale = -1;
        }

    }

    int GetPlayersWithTag(int numb)
    {
        int count = 0;
        for (int i = 0; i < numb; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.CompareTag("Player"))
                count++;
        }
        return count;
    }
    void UpdateLayout()
    {
        //int NumberPlayer = GetPlayersWithTag(this.transform.childCount);
        int NumberPlayer = this.transform.childCount;
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
