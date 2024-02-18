using UnityEngine;

namespace hungw
{
    public class LineImage : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        private float currentSize, size;

        // Start is called before the first frame update
        void Start()
        {
            currentSize = Vector2.Distance(this.transform.position, origin.position);
        }

        // Update is called once per frame
        void Update()
        {
            size = Vector2.Distance(this.transform.position, origin.position) / currentSize;
            this.transform.localScale = new Vector3(transform.localScale.x, size, transform.localScale.z);
        }
    }
}
