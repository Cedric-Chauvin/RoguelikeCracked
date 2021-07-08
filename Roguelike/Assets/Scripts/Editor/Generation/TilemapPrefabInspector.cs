using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilemapPrefabs))]
public class TilemapPrefabInspector : Editor
{
    private TilemapPrefabs tilemapPrefabs;

    private void OnEnable()
    {
        tilemapPrefabs = FindObjectOfType<TilemapPrefabs>();
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Update Info"))
        {
            (serializedObject.targetObject as TilemapPrefabs).UpdateInfo();
        }
        base.OnInspectorGUI();
    }
}
