using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GroundWallGeneration))]
public class GroundWallInspector : Editor
{
    private GroundWallGeneration generator;

    private void OnEnable()
    {
        generator = FindObjectOfType<GroundWallGeneration>();
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
