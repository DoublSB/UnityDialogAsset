/*
The MIT License

Copyright (c) 2020 DoublSB
https://github.com/DoublSB/UnityDialogAsset

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Doublsb.Dialog
{
    [CustomPropertyDrawer(typeof(Emotion))]
    public class EmotionDrawer : PropertyDrawer
    {
        #region variables
        //================================================
        //Private Variable
        //================================================
        private int ArraySize = 0;
        private string EmotionName = "Input the emotion name";

        private SerializedProperty _emotion = null;
        private SerializedProperty _sprite = null;
        #endregion

        #region override
        //================================================
        //Public Method
        //================================================
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            _initialize(position, property);
            _display_Header(position);
            _display_EmotionList(position);
            _display_AddArea(position);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 18 * (ArraySize + 2);
        }
        #endregion

        #region init
        //================================================
        //Private Method : init
        //================================================
        private void _initialize(Rect pos, SerializedProperty property)
        {
            _emotion = property.FindPropertyRelative("_emotion");
            _sprite = property.FindPropertyRelative("_sprite");

            ArraySize = _emotion.arraySize;
        }
        #endregion

        #region display
        //================================================
        //Private Method : display
        //================================================
        private void _display_Header(Rect startPos)
        {
            EditorGUI.LabelField(startPos, "Emotion");
            EditorGUI.indentLevel++;
        }

        private void _display_Array(Rect startPos, SerializedProperty array)
        {
            for (int i = 0; i < array.arraySize; i++)
            {
                startPos = new Rect(startPos.position + new Vector2(0, 18), startPos.size);
                EditorGUI.PropertyField(startPos, array.GetArrayElementAtIndex(i), GUIContent.none);
            }
        }

        private void _display_DeleteButton(Rect startPos)
        {
            for (int i = 0; i < _sprite.arraySize; i++)
            {
                startPos = new Rect(startPos.position + new Vector2(0, 18), startPos.size);
                if(_emotion.GetArrayElementAtIndex(i).stringValue != "Normal" && GUI.Button(startPos, "-"))
                { 
                    int j = i;
                    _delete_Raw(j);
                }
            }
        }

        private void _display_EmotionList(Rect startPos)
        {
            Rect NewRect = new Rect(startPos.position, new Vector2(startPos.width / 3, 16));

            _display_Array(NewRect, _emotion);
            _display_Array(_get_Rect(NewRect, NewRect.width, NewRect.width), _sprite);
            _display_DeleteButton(_get_Rect(NewRect, NewRect.width * 2 + 10, 30));
        }

        private void _display_AddButton(Rect rect)
        {
            if (GUI.Button(rect, "create"))
            {
                _add_Raw();
                EmotionName = "";
            }
        }

        private void _display_TextArea(Rect rect)
        {
            EmotionName = EditorGUI.TextField(rect, EmotionName);
        }

        private void _display_AddArea(Rect startPos)
        {
            Rect InputRect = _get_Rect(startPos, 0, startPos.width / 3 * 2, (_emotion.arraySize + 1) * 18);

            _display_TextArea(InputRect);
            _display_AddButton(_get_Rect(InputRect, InputRect.width + 20, 70));
        }
        #endregion

        #region methods
        //================================================
        //Private Method : methods
        //================================================
        private void _delete_ArrayElement(SerializedProperty array, int index, bool isObject = false)
        {
            if (isObject && array.GetArrayElementAtIndex(index) != null) array.DeleteArrayElementAtIndex(index);
            array.DeleteArrayElementAtIndex(index);
        }

        private void _delete_Raw(int index)
        {
            _delete_ArrayElement(_emotion, index);
            _delete_ArrayElement(_sprite, index, true);
        }

        private void _add_Raw()
        {
            if (!_is_duplicated_emotion_name(EmotionName))
            {
                _emotion.InsertArrayElementAtIndex(_emotion.arraySize);
                _emotion.GetArrayElementAtIndex(_emotion.arraySize - 1).stringValue = EmotionName;

                _sprite.InsertArrayElementAtIndex(_sprite.arraySize);
            }
        }

        private bool _is_duplicated_emotion_name(string name)
        {
            for (int i = 0; i < _emotion.arraySize; i++)
            {
                if (_emotion.GetArrayElementAtIndex(i).stringValue == name) return true;
            }

            return false;
        }

        private Rect _get_Rect(Rect From, float x, float width)
        {
            return new Rect(From.position + new Vector2(x, 0), new Vector2(width, 16));
        }

        private Rect _get_Rect(Rect From, float x, float width, float y)
        {
            return new Rect(From.position + new Vector2(x, y), new Vector2(width, 16));
        }
        #endregion
    }
}
#endif