using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LuoiCauScript : MonoBehaviour {
	public float speed;
	public float speedMin;
	public float speedBegin;
	public Vector2 velocity;
	public float maxX;
	public float minX;
	public float minY;
	public float maxY;
	public Transform target;
	Vector3 prePosition;

	public int type;

	public bool isUpSpeed;
	public float timeUpSpeed;
	private DayCauScript daycau;
	// Use this for initialization
	void Start () {
		isUpSpeed = false;
		prePosition = transform.localPosition;
		daycau = GetComponentInParent<DayCauScript>();
    }

	public IEnumerator TimeUpSpeed ()
	{
		while(true){
			if(isUpSpeed)
			{
				timeUpSpeed = timeUpSpeed - 1;
				if(timeUpSpeed <= 0)
					isUpSpeed = false;
			}
			yield return new WaitForSeconds (1);
		}
	}

	bool checkPositionOutBound() {
		return  gameObject.GetComponent<Renderer>().isVisible ;
	}

    public void checkTouchScene() {
		if(daycau.typeAction == TypeAction.Nghi)
		{
            this.GetComponent<CircleCollider2D>().enabled = true;
            daycau.typeAction = TypeAction.ThaCau;
			velocity = new Vector2(transform.position.x - target.position.x, 
			                       transform.position.y - target.position.y);
			velocity.Normalize();
			speed = speedBegin;
		}
	}
    //kiem tra khi luoi cau ra ngoai tam nhin cua camera
    public void checkMoveOutCameraView() {
		if(daycau.typeAction == TypeAction.ThaCau) {
			if(!checkPositionOutBound()) {
				daycau.typeAction = TypeAction.KeoCau;
				this.GetComponent<CircleCollider2D>().enabled = false;
				velocity = -velocity;
			}
		}
	}

	//kiem tra khi luoi ca keo len mat nuoc
	public void checkKeoCauXong() {
		if(transform.localPosition.y > maxY && daycau.typeAction == TypeAction.KeoCau) {
			Debug.Log("keo cau xong");
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			daycau.typeAction = TypeAction.Nghi;
			transform.localPosition = prePosition;
			
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
			if (!other.TryGetComponent<Item>(out Item item))
			{
				other.gameObject.AddComponent<Item>();
				StartCoroutine(ZoomInOut(other.gameObject));
            }
        }
    }

	IEnumerator ZoomInOut(GameObject other)
	{
		other.SetActive(false);
		yield return new WaitForSeconds(5);
		other.SetActive(true);
		try
		{
            Destroy(other.GetComponent<Item>(), 5);
        }
		catch { }
    }

}
