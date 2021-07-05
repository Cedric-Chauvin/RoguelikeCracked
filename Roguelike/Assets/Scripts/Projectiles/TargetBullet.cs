using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour, IPooledObject
{
    PlayerManager PManager;

    public void OnObjectSpawn()
    {
        if (PManager == null)
        {
            PManager = FindObjectOfType<PlayerManager>();
        }

        StartCoroutine(bulletLoop(PManager.BulletDuration));
    }

    private IEnumerator bulletLoop(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemie : " + collision.gameObject.name + " touché !");
        }

        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
