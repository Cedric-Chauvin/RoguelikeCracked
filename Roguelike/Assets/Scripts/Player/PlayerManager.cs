using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Stats")]
    public float PlayerSpeed;
    public float VisionRange;
    public float PlayerSize;
    public float DetectionRange;

    [Header("Camera Stats")]
    public float CameraFOV;

    [Header("Bullets Stats")]
    public float BulletDamage;
    public float BulletFireRate;
    public float BulletSpeed;
    public float BulletDuration;
    public GameObject BulletSkin;
    
}
