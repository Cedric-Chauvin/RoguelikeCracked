using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    private Animator EntityAnim;
    private CircleCollider2D DetectionCollider;
    private PolygonCollider2D InRangeCollider;

    private void Start()
    {
        EntityAnim = GetComponent<Animator>();
        DetectionCollider = GetComponent<CircleCollider2D>();
        InRangeCollider = GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        //EntityAnim.SetFloat("PlayerPosition",-45);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Player = collision.gameObject;
        float angle = Vector2.Angle(Vector2.down, Player.transform.position - transform.position);

        if (collision.tag == "Player" && EntityAnim.GetBool("Awake") == false)
        {
            EntityAnim.SetBool("Awake", true);
        }
        else if (collision.tag == "Player" && EntityAnim.GetBool("Awake") == true)
        {
            EntityAnim.SetBool("InRange", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && EntityAnim.GetBool("InRange") == true)
        {
            EntityAnim.SetBool("InRange", false);
        }
        else if (collision.tag == "Player" && EntityAnim.GetBool("Awake") == true)
        {
            EntityAnim.SetBool("Awake", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (EntityAnim.GetBool("InRange") == true && collision.tag == "Player")
        {
            GameObject Player = collision.gameObject;
            float angle = Vector2.Angle(Vector2.down, Player.transform.position - transform.position);
            if (Player.transform.position.x < transform.position.x)
            {
                angle = -angle;
            }
            EntityAnim.SetFloat("PlayerPosition", angle);
        }
    }
}
