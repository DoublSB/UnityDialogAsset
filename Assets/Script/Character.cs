﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Doublsb.Dialog
{
    [RequireComponent(typeof(Image))]
    public class Character : MonoBehaviour
    {
        [HideInInspector]
        public Sprite[] Emotions;
        public AudioClip[] SE;
    }

    [CustomEditor(typeof(Character))]
    public class CharacterEditor : Editor
    {
        Character OriginalScript = null;

        private void OnEnable()
        {
            OriginalScript = (Character)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Emotion[] emotions = (Emotion[])Enum.GetValues(typeof(Emotion));

            //Exception catch when null
            if (OriginalScript.Emotions == null)
                OriginalScript.Emotions = new Sprite[emotions.Length];

            //Exception catch when length is different
            if (emotions.Length > OriginalScript.Emotions.Length)
                Array.Resize(ref OriginalScript.Emotions, emotions.Length);

            for (int i = 0; i < emotions.Length; i++)
            {
                OriginalScript.Emotions[i]
                    = (Sprite)EditorGUILayout.ObjectField(emotions[i].ToString(), OriginalScript.Emotions[i], typeof(Sprite), true);
            }
        }
    }
}