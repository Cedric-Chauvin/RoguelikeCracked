using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/New Enemy", fileName = "New Enemy")]
public class EnemyStats : ScriptableObject
{
    public float EnemyHP;
    public float EnemyDamage;
    public float EnemySpeed;
}
