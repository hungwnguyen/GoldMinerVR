using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HorizontalLayout : MonoBehaviour
{
    [SerializeField] private float spacing = 1f;

    private int currentChild, childCount;
    
    private void Start()
    {
        this.currentChild = 0;
    }
    private void Update()
    {
        childCount = transform.childCount;
        if (childCount != currentChild && GameManager.Instance.PlayMode != GameManager.eGameState.ENDROUND)
        {
            currentChild = childCount;
            SpaceChildObjects();
        }
        if (GameManager.Instance.PlayMode == GameManager.eGameState.ENDROUND)
        {
            Destroy(this.GetComponent<HorizontalLayout>());
        }
    }

    private void SpaceChildObjects()
    {
      
        float totalWidth = (childCount - 1) * spacing;
        float startPosition = transform.position.x - totalWidth / 2;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.position = new Vector3(startPosition + i * spacing, transform.position.y, child.position.z);
        }
    }
}
