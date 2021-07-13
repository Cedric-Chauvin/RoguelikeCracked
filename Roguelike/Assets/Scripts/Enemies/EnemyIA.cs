using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyIA : MonoBehaviour
{
    public EnemyStats Stats;
    public PlayerManager P_Manager;
    private GameObject Player;

    private Vector2 PlayerPosition;
    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;

    private float EnemyHP;
    public float EnemyDamage;

    private void Start()
    {
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
            if (EnemyHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
