using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Stats")]
    public float PlayerSpeed;
    public float PlayerHP;
    public float PlayerSize;
    public float PlayerInvincibility;
    public float VisionRange;
    public float DetectionRange;
    public int NbReroll;

    [Header("Player Relics")]
    public List<Relic> PlayerRelic;

    [Header("Camera Stats")]
    public float CameraFOV;

    [Header("Bullets Stats")]
    public float BulletDamage;
    public float BulletFireRate;
    public float BulletSpeed;
    public float BulletDuration;
    public GameObject BulletSkin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EnemyIA Enemy = other.gameObject.GetComponent<EnemyIA>();
            StartCoroutine("CooldownHitPlayer", Enemy);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            StopCoroutine("CooldownHitPlayer");
        }
    }

    IEnumerator CooldownHitPlayer(EnemyIA Enemy)
    {
        while (true)
        {
            PlayerHP -= Enemy.EnemyDamage;
            if (PlayerHP <= 0)
            {
                this.gameObject.SetActive(false);
                StopCoroutine("CooldownHitPlayer");
                SceneManager.LoadScene(0);
            }
            yield return new WaitForSeconds(PlayerInvincibility);
        }
    }

    public void ApplyRelic(Relic SelectedRelic)
    {
        PlayerSize = PlayerSize * (1 + SelectedRelic.ModifSize);
        PlayerSpeed = PlayerSpeed * (1 + SelectedRelic.ModifSpeed);
        transform.localScale = new Vector3(transform.localScale.x * PlayerSize, transform.localScale.y * PlayerSize, 1);
        CameraFOV = CameraFOV * (1 + SelectedRelic.ModifFOV);
        BulletFireRate = BulletFireRate * (1 + SelectedRelic.ModifBulletFireRate);
        BulletDamage = BulletDamage * (1 + SelectedRelic.ModifBulletDamage);
        BulletSpeed = BulletSpeed * (1 + SelectedRelic.ModifBulletSpeed);
        BulletDuration = BulletDuration * (1 + SelectedRelic.ModifBulletDuration);
        DetectionRange = DetectionRange * (1 + SelectedRelic.ModifDetectionRange);
    }
}
