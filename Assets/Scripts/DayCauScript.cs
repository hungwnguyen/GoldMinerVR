using UnityEngine;
using System.Collections;

public enum TypeAction { Nghi, ThaCau, KeoCau };

public class DayCauScript : MonoBehaviour {
    public Transform luoiCau;
	public Transform target;
	public Vector3 angles;
	public float speed = 5;
	public float angleMax = 70;
	public TypeAction typeAction = TypeAction.Nghi;
	private Vector3 initAngles;
    public LineRenderer lineRenderer;
    public float thickness = 0.1f;

    // Use this for initialization
    void Start () {
		lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
        initAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
	}

	void pendulum(float x, float y, float z) {
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * speed) * angleMax);
	}
}
