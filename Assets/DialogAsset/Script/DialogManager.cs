using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Doublsb.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [Header("Object Mapping")]
        public Text text;
        public AudioSource Audio;

        public Character character;

        [HideInInspector]
        public State state;

        private float SpeakTime;

        private void Start()
        {
            Show("이것은 대화창이다. [Sad]하지만 '언젠가는' 완성되겠지. [Happy]^O^", character);
        }

        public void Show(string Text, Character character)
        {
            Initialize(null);
            StartCoroutine(Texting(Text, character));
        }

        private void Initialize(Image image)
        {
            SpeakTime = 0.1f;

            if (image != null)
            {
                image.sprite = null;
                image.color = new Color(0, 0, 0, 0);
            }

            text.text = "";
        }


        #region Show Text

        private IEnumerator Texting(string Text, Character character)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '[')
                {
                    string SubStringResult = Text.Substring(i + 1);
                    int charLength = Get_CharIndexLength(SubStringResult, ']');

                    Show_Emotion(SubStringResult.Substring(0, charLength), character);

                    i += charLength + 1;
                }
                else
                {
                    text.text += Text[i];
                }

                if (Text[i] != ' ') Play_SE(character);

                yield return new WaitForSeconds(SpeakTime);
            }
        }

        private int Get_CharIndexLength(string Text, char chr)
        {
            return Text.IndexOf(chr);
        }

        private void Show_Emotion(string Text, Character character)
        {
            character.GetComponent<Image>().sprite = character.Emotion.Data[Text];
        }

        #endregion

        #region Sound

        private void Play_SE(Character character)
        {
            Audio.clip = character.SE[UnityEngine.Random.Range(0, character.SE.Length)];
            Audio.Play();
        }

        #endregion
    }

    public class CharacterDatabase
    {
        public CharacterDatabase(string JsonData)
        {
            JsonUtility.FromJson<CharacterDatabase>(JsonData);
        }

        public void Add()
        {

        }
    }
}