using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    private PlayerManager P_Manager;

    private bool IsInRange = false;

    private float Distance = -1f;
    private Transform Target;

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
            foreach (var collider in colls)
            {
                float dist = Vector2.Distance(transform.position, collider.transform.position);

                if (Distance == -1)
                {
                    Distance = dist;
                    Target = collider.transform;
                }
                else if (dist < Distance)
                {
                    Distance = dist;
                    Target = collider.transform;

                    //Debug.Log("Nouvelle target : " + collider.gameObject.name);
                }
                else if (Target != null && collider.gameObject == Target.gameObject)
                {
                    Distance = dist;
                }
                else
                {
                    Distance = -1;
                    IsInRange = false;
                }
            }

            IsInRange = true;
        }
        else
        {
            Distance = -1;
            Target = null;
            IsInRange = false;
            //Debug.Log("Pas d'ennemies");
        }
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
