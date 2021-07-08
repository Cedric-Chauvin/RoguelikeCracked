using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    private PlayerManager P_Manager;

    private bool IsInRange = false;

    private float Distance = -1f;
    public Transform Target;

    ObjectPooler objectPooler;

    private void Start()
    {
        IsInRange = false;
        P_Manager = gameObject.GetComponent<PlayerManager>();
        objectPooler = ObjectPooler.Instance;
        StartCoroutine(AutoFire());
    }

    private void Update() //Tout l'update sert à définir la cible la plus proche
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, P_Manager.DetectionRange);
        List<Collider2D> colls = new List<Collider2D>();

        foreach(var col in collisions)
        {
            if (col.CompareTag("Enemy"))
            {
                colls.Add(col);
            }
        }

        //Debug.Log(colls.Count);

        if (colls.Count > 0)
        {
            if (Target) //initialise la distance si il y a deja une target
            {
                Distance = Vector2.Distance(transform.position, Target.position);
                if (Distance > P_Manager.DetectionRange || !isInSight(Target))//si la target est hors range ou hors vue
                {
                    Distance = -1; 
                    IsInRange = false;
                    Target = null;
                }
            }
            foreach (var collider in colls)
            {
                if (Target != null && collider.gameObject == Target.gameObject)
                    continue; //si on tombe sur la target pas besoin de check

                float dist = Vector2.Distance(transform.position, collider.transform.position);

                if (Distance == -1)
                {
                    if (isInSight(collider.transform))
                    {
                        Distance = dist;
                        Target = collider.transform;
                        IsInRange = true;
                    }
                }
                else if (dist < Distance)
                {
                    if (isInSight(collider.transform))
                    {
                        Distance = dist;
                        Target = collider.transform;
                        IsInRange = true;
                    }

                    //Debug.Log("Nouvelle target : " + collider.gameObject.name);
                }
            }
        }
        else
        {
            Distance = -1;
            Target = null;
            IsInRange = false;
            //Debug.Log("Pas d'ennemies");
        }
    }

    private bool isInSight(Transform enemy)
    {
        int Count = 0;
        RaycastHit2D[] hit = Physics2D.LinecastAll(transform.position, enemy.position);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag != "Wall")
            {
                Count++;
            }
        }
        return Count == hit.Length;
    }
    IEnumerator AutoFire()
    {
        while (true)
        {
           yield return new WaitForSeconds(P_Manager.BulletFireRate);
           Transform currentBullet;

            /*string[] projectils = { "BaseBullet", "FireBullet", "Fromage", "Patate" }; //Pour les bullets random

            int Rand = Random.Range(0, 4);

            string projectil = projectils[Rand];*/
            
            if (IsInRange == true && Target != null)
            {
                currentBullet = objectPooler.SpawnFromPool("BaseBullet", transform.position, Quaternion.identity).transform;
                currentBullet.GetComponent<Rigidbody2D>().AddForce((Target.position - transform.position) * P_Manager.BulletSpeed);
            }
            else
            {
                yield return null;
            }
        }
    }
}
