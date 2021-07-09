using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Relics/New Relic", fileName = "New Relic")]
public class Relic : ScriptableObject
{   
    [Header("Player Stats")]
    public float ModifSpeed;
    public float ModifSize;  
    public float ModifFOV;

    [Header("Bullets Stats")]
    public float ModifBulletFireRate;
    public float ModifBulletDamage;
    public float ModifDetectionRange;
    public float ModifBulletDuration;
    public float ModifBulletSpeed;
    public GameObject BulletSkin;

    [Header("Relic Description")]
    public Sprite RelicSprite;
    public string RelicName;
    public string RelicDescription;
    public string RelicLVLText;
    public int RelicLVL;
}
