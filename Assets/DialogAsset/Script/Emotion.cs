using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doublsb.Dialog
{
    [SerializeField]
    public class Emotion : MonoBehaviour
    {
        //================================================
        //Public Variable
        //================================================
        public Dictionary<string, Sprite> Data;


        //================================================
        //Private Variable
        //================================================
        [SerializeField]
        private string[] _emotion;

        [SerializeField]
        private Sprite[] _sprite;


        //================================================
        //Private Method
        //================================================
        private void Awake()
        {
            _init_emotionList();
        }

        private void _init_emotionList()
        {
            Data = new Dictionary<string, Sprite>();

            if (_emotion.Length != _sprite.Length)
                Debug.LogError("Emotion and Sprite have different lengths");

            for (int i = 0; i < _emotion.Length; i++)
                Data.Add(_emotion[i], _sprite[i]);
        }
    }
}