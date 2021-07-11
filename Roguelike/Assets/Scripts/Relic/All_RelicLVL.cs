using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Relics/New All_RelicLVL", fileName = "New All_RelicLVL")]
public class All_RelicLVL : ScriptableObject
{
    public List<RelicLVL_List> List_AllRelicsLVL = new List<RelicLVL_List>();
}
