using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyIA : MonoBehaviour
{
    public EnemyStats Stats;
    public PlayerManager P_Manager;
    private GameObject Player;
    public Material FlashHitMaterial;
    private Material StartMaterial;

    private Vector2 PlayerPosition;
    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;

    private float EnemyHP;
    public float EnemyDamage;

    private void Start()
    {
        StartMaterial = GetComponent<SpriteRenderer>().material;
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPath.maxSpeed = Stats.EnemySpeed;
        Player = GameObject.Find("Player");
        P_Manager = Player.GetComponent<PlayerManager>();
        aiDestinationSetter.target = Player.transform;
        EnemyHP = Stats.EnemyHP;
        EnemyDamage = Stats.EnemyDamage;
    }

    private void Update()
    {
        if (Player != null)
        {
            //PlayerPosition = Player.transform.position;
            //float step = Stats.EnemySpeed * Time.deltaTime;
            //transform.position = Vector2.MoveTowards(gameObject.transform.position, PlayerPosition, step);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            other.gameObject.SetActive(false);
            EnemyHP -= P_Manager.BulletDamage;
            StartCoroutine(FlashAnim());
            if (EnemyHP <= 0)
            {
                P_Manager.KillCount++;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator FlashAnim()
    {
        GetComponent<SpriteRenderer>().material = FlashHitMaterial;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material = StartMaterial;
        StopCoroutine(FlashAnim());
    }
}
