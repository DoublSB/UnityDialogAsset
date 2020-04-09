using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;

namespace Doublsb.Dialog
{
    [CustomPropertyDrawer(typeof(Emotion))]
    public class EmotionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty _emotion = property.FindPropertyRelative("_emotion");

            EditorGUI.BeginProperty(position, label, property);

            // {

            Rect NewRect = new Rect(position.position, new Vector2(position.width, 16));

            for (int i = 0; i < _emotion.arraySize; i++)
            {
                NewRect = new Rect(NewRect.position + new Vector2(0, 20), NewRect.size);
                EditorGUI.PropertyField(NewRect, _emotion.GetArrayElementAtIndex(i));
            }

            // }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? 16 * 10 : 16;
        }
    }
}
#endif