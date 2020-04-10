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
        private int ArraySize = 0;
        private string EmotionName = "Input the emotion name";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty _emotion = property.FindPropertyRelative("_emotion");
            SerializedProperty _sprite = property.FindPropertyRelative("_sprite");

            ArraySize = _emotion.arraySize;

            EditorGUI.BeginProperty(position, label, property);

            // {

            EditorGUI.LabelField(position, "Emotion");

            EditorGUI.indentLevel++;

            Rect NewRect = new Rect(position.position, new Vector2(position.width / 3, 16));

            for (int i = 0; i < _emotion.arraySize; i++)
            {
                NewRect = new Rect(NewRect.position + new Vector2(0, 18), NewRect.size);
                EditorGUI.PropertyField(NewRect, _emotion.GetArrayElementAtIndex(i), GUIContent.none);
            }

            NewRect = new Rect(position.position + new Vector2(NewRect.width, 0), new Vector2(NewRect.width, 16));

            for (int i = 0; i < _sprite.arraySize; i++)
            {
                NewRect = new Rect(NewRect.position + new Vector2(0, 18), NewRect.size);
                EditorGUI.PropertyField(NewRect, _sprite.GetArrayElementAtIndex(i), GUIContent.none);
            }

            NewRect = new Rect(position.position + new Vector2(NewRect.width * 2 + 10, 0), new Vector2(30, 16));

            for (int i = 0; i < _sprite.arraySize; i++)
            {
                NewRect = new Rect(NewRect.position + new Vector2(0, 18), NewRect.size);
                if(GUI.Button(NewRect, "-"))
                {
                    int j = i;
                    _emotion.DeleteArrayElementAtIndex(j);

                    if (_sprite.GetArrayElementAtIndex(j).objectReferenceValue != null)
                        _sprite.DeleteArrayElementAtIndex(j);

                    _sprite.DeleteArrayElementAtIndex(j);
                }
            }

            NewRect = new Rect(position.position + new Vector2(0, 18 * _emotion.arraySize + 30), new Vector2(position.width / 3 * 2, 16));

            EmotionName = EditorGUI.TextField(NewRect, EmotionName);

            if(GUI.Button(new Rect(NewRect.position + new Vector2(NewRect.width + 10, 0), new Vector2(70, 16)), "create"))
            {
                _emotion.InsertArrayElementAtIndex(_emotion.arraySize);
                _emotion.GetArrayElementAtIndex(_emotion.arraySize - 1).stringValue = EmotionName;

                _sprite.InsertArrayElementAtIndex(_sprite.arraySize);

                if(_sprite.GetArrayElementAtIndex(_sprite.arraySize - 1).objectReferenceValue != null)
                    _sprite.DeleteArrayElementAtIndex(_sprite.arraySize - 1);

                EmotionName = "";
            }

            // }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 16 * (ArraySize + 4);
        }
    }
}
#endif