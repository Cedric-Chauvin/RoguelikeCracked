using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Relics/New RelicLVL_List", fileName = "New RelicLVL_List")]
public class RelicLVL_List : ScriptableObject
{
    public List<Relic> List_RelicsLVL = new List<Relic>();
}
