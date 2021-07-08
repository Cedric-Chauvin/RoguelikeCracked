using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGeneration))]
public class GroundWallInspector : Editor
{
    private MapGeneration generator;

    private void OnEnable()
    {
        generator = FindObjectOfType<MapGeneration>();
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Regenerate map"))
        {
            generator.Clear();
            generator.Spawn();
        }
        base.OnInspectorGUI();
    }

}
