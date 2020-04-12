using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Doublsb.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        //================================================
        //Public Variable
        //================================================
        [Header("Object Mapping")]
        public GameObject Window;
        public Text text;
        public AudioSource Audio;

        public Character character;

        [HideInInspector]
        public State state;


        //================================================
        //Private Method
        //================================================
        private float SpeakTime;


        //================================================
        //Public Method
        //================================================
        public void Show(string Text, Character character)
        {
            Window.SetActive(true);
            Initialize(null);
            StartCoroutine(Texting(Text, character));
        }

        public void Click_Window()
        {
            switch (state)
            {
                case State.Texting:
                    StartCoroutine(Skip()); break;

                case State.WaitForInput:
                    Hide(); break;
            }
        }

        public void Hide()
        {
            StopAllCoroutines();
            Window.SetActive(false);
        }

        //================================================
        //Private Method
        //================================================
        private void Start()
        {
            Show("이것은 대화창이다. /emote:Sad/하지만 '언젠가는' 완성되겠지. /emote:Happy/^O^", character);
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
            state = State.Texting;

            text.text = string.Empty;
            var dialogText = new DialogText(Text);

            foreach (var item in dialogText.Commands)
            {
                switch (item.Command)
                {
                    case Command.text:
                        yield return StartCoroutine(_showText(item.Context));
                        break;
                    case Command.color:
                        break;
                    case Command.emote:
                        Show_Emotion(item.Context, character);
                        break;
                }
            }

            state = State.WaitForInput;
        }

        private IEnumerator _showText(string Text)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                text.text += Text[i];

                if (Text[i] != ' ') Play_SE(character);
                if (SpeakTime != 0) yield return new WaitForSeconds(SpeakTime);
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

        private IEnumerator Skip()
        {
            SpeakTime = 0;
            while (state != State.WaitForInput) yield return null;
            SpeakTime = 1;
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
}