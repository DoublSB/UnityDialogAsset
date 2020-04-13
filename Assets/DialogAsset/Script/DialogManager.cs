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
        public DialogText CurrentText;


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
            Show("/color:red/^O^/size:down/hi", character);
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
            CurrentText = new DialogText(Text, text.fontSize);

            foreach (var item in CurrentText.Commands)
            {
                switch (item.Command)
                {
                    case Command.text:
                        yield return StartCoroutine(_showText(item.Context));
                        break;
                    case Command.color:
                        _coloring(item.Context);
                        break;
                    case Command.emote:
                        Show_Emotion(item.Context, character);
                        break;
                    case Command.size:
                        _sizing(item.Context);
                        break;
                }
            }

            state = State.WaitForInput;
        }

        private IEnumerator _showText(string Text)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                CurrentText.PrintText += Text[i];
                text.text = CurrentText.PrintText + CurrentText.BackText;

                if (Text[i] != ' ') Play_SE(character);
                if (SpeakTime != 0) yield return new WaitForSeconds(SpeakTime);
            }
        }

        private void _coloring(string Color)
        {
            int existIndex = CurrentText.CloseCommands.LastIndexOf(Command.color);
            if (existIndex > 0)
            {
                CurrentText.CloseCommands.RemoveAt(existIndex);
                CurrentText.PrintText += "</color>";
            }

            CurrentText.PrintText += $"<color={Color}>";
            CurrentText.CloseCommands.Add(Command.color);
        }

        private void _sizing(string Size)
        {
            int existIndex = CurrentText.CloseCommands.LastIndexOf(Command.size);
            if (existIndex > 0)
            {
                CurrentText.CloseCommands.RemoveAt(existIndex);

                switch (Size)
                {
                    case "up":
                        CurrentText.Size += 2;
                        break;

                    case "down":
                        CurrentText.Size -= 5;
                        break;

                    case "init":
                        CurrentText.Size = text.fontSize;
                        break;

                    default:
                        CurrentText.Size = int.Parse(Size);
                        break;
                }

                CurrentText.PrintText += "</size>";
            }

            CurrentText.PrintText += $"<size={CurrentText.Size}>";
            CurrentText.CloseCommands.Add(Command.size);
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