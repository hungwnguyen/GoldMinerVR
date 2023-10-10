using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isMoveFollow = false;
    public float maxY;
    public int score;
    public float speed;

    private DayCauScript daycau;
    private LuoiCauScript luoi;

    // Use this for initialization
    void Start()
    {
        isMoveFollow = false;
    }

    void FixedUpdate()
    {
        if (isMoveFollow)
        {
            moveFllowTarget(luoi.transform);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "luoiCau")
        {
            isMoveFollow = true;
            luoi = other.gameObject.GetComponent<LuoiCauScript>();
            daycau = luoi.transform.GetComponentInParent<DayCauScript>();
            luoi.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            daycau.GetComponent<DayCauScript>().typeAction = TypeAction.KeoCau;
            luoi.velocity = -luoi.GetComponent<LuoiCauScript>().velocity;
            luoi.GetComponent<LuoiCauScript>().speed -= this.speed;
        }
    }

    void moveFllowTarget(Transform target)
    {
       
        Quaternion tg = Quaternion.Euler(target.parent.transform.rotation.x,
                                             target.parent.transform.rotation.y,
                                             90 + target.parent.transform.rotation.z);
        //				transform.rotation = Quaternion.Slerp(transform.rotation, tg, 0.5f);
        transform.position = new Vector3(target.position.x,
                                         target.position.y - gameObject.GetComponent<Collider2D>().GetComponent<Collider2D>().bounds.size.y / 2,
                                         transform.position.z);
        if (daycau.typeAction == TypeAction.Nghi)
        {
            PlayerManager player = daycau.GetComponentInParent<PlayerManager>();
            player.UpdateScore(this.score);
            if (this.CompareTag("TNT"))
            {
                this.GetComponent<Animator>().SetBool("tnt", true);
                Destroy(gameObject, 0.667f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
