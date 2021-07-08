using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CustomSliderAttribute))]
public class CustomSliderDrawer : PropertyDrawer
{

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label)*2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // First get the attribute since it contains the range for the slider
        CustomSliderAttribute myAttribute = (CustomSliderAttribute)attribute;

        //Get texture size to setup max size
        var maxProperty = property.serializedObject.FindProperty(myAttribute.max);
        Texture2D realTexture = null;
        if (maxProperty.propertyType == SerializedPropertyType.ObjectReference)
             realTexture = maxProperty.objectReferenceValue as Texture2D;
        int max = realTexture.width > realTexture.height ? realTexture.width : realTexture.height;

        if(property.propertyType == SerializedPropertyType.Vector2Int)
        {
            Vector2Int value = property.vector2IntValue;
            position.height /= 2;
            int width = value.x;
            int height = value.y;
            width = EditorGUI.IntSlider(position,"MapWidth", width, 20, realTexture.width);
            position.y+= 20;

            height = EditorGUI.IntSlider(position, "MapHeight", height, 20, realTexture.height);
            property.vector2IntValue = new Vector2Int(width, height);
        }
        else
            EditorGUI.LabelField(position, label.text, "Use MyRange with vectorint.");
    }
}
