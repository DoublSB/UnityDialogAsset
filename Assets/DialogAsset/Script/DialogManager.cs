using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

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
        public AudioSource SEAudio;
        public AudioSource CallAudio;

        public Character character;

        [HideInInspector]
        public State state;
        public DialogText CurrentText;
        public bool cannotSkip;

        //================================================
        //Private Method
        //================================================
        private float SpeakTime;
        private float LastSpeakTime;
        private UnityAction _callback;
        private Coroutine _textingRoutine;

        //================================================
        //Public Method
        //================================================
        public void Show(List<string> Text, Character character, bool CannotSkip = false, UnityAction Callback = null)
        {
            Window.SetActive(true);
            Initialize(null);
            StartCoroutine(Texting(Text, character, CannotSkip, Callback));
        }

        public void Show(string Text, Character character, bool CannotSkip = false, UnityAction Callback = null)
        {
            _callback = Callback;

            Window.SetActive(true);
            Initialize(null);
            _textingRoutine = StartCoroutine(Texting(Text, character, CannotSkip));
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
            StopCoroutine(_textingRoutine);
            Window.SetActive(false);

            if (_callback != null) _callback.Invoke();
            else state = State.Hide;
        }

        //================================================
        //Private Method
        //================================================
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

        private IEnumerator Texting(List<string> Text, Character character, bool CannotSkip = false, UnityAction Callback = null)
        {
            foreach (var item in Text)
            {
                Show(item, character, CannotSkip);

                while (state != State.Hide) { yield return null; }
            }

            if(Callback != null) Callback.Invoke();
        }

        private IEnumerator Texting(string Text, Character character, bool CannotSkip = false)
        {
            LastSpeakTime = 0.1f;
            state = State.Texting;
            cannotSkip = CannotSkip;

            text.text = string.Empty;
            CurrentText = new DialogText(Text);

            foreach (var item in CurrentText.Commands)
            {
                switch (item.Command)
                {
                    case Command.text:
                        if(item.Context != string.Empty) yield return StartCoroutine(_showText(item.Context));
                        break;

                    case Command.color:
                        CurrentText.Color = item.Context;
                        if (CurrentText.Size == string.Empty) CurrentText.Size = text.fontSize.ToString();
                        break;

                    case Command.emote:
                        Show_Emotion(item.Context, character);
                        break;

                    case Command.size:
                        _sizing(item.Context);
                        break;

                    case Command.sound:
                        Play_CallSE(character, item.Context);
                        break;

                    case Command.speed:
                        Set_Speed(item.Context);
                        break;

                    case Command.click:
                        yield return _waitInput();
                        break;

                    case Command.close:
                        Hide();
                        yield break;

                    case Command.wait:
                        yield return new WaitForSeconds(float.Parse(item.Context));
                        break;
                }
            }

            state = State.WaitForInput;
            cannotSkip = false;
        }

        private IEnumerator _waitInput()
        {
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            SpeakTime = LastSpeakTime;
        }

        private void _addOpenTagger()
        {
            if (CurrentText.Color != string.Empty && CurrentText.Size != string.Empty)
            {
                if (CurrentText.hasOpenTagger) CurrentText.PrintText += CurrentText.CloseTagger;
                CurrentText.PrintText += CurrentText.OpenTagger;

                CurrentText.hasOpenTagger = true;
            }
        }

        private void _addCloseTagger()
        {
            if (CurrentText.Color != string.Empty && CurrentText.Size != string.Empty)
            {
                text.text += CurrentText.CloseTagger;
            }
        }

        private IEnumerator _showText(string Text)
        {
            _addOpenTagger();

            for (int i = 0; i < Text.Length; i++)
            {
                CurrentText.PrintText += Text[i];
                text.text = CurrentText.PrintText;
                _addCloseTagger();

                if (Text[i] != ' ') Play_ChatSE(character);
                if (SpeakTime != 0) yield return new WaitForSeconds(SpeakTime);
            }
        }

        private void _sizing(string Size)
        {
            if (CurrentText.Size == string.Empty) CurrentText.Size = text.fontSize.ToString();
            if (CurrentText.Color == string.Empty) CurrentText.Color = "white";

            switch (Size)
            {
                case "up":
                    CurrentText.Size = (int.Parse(CurrentText.Size) + 10).ToString();
                    break;

                case "down":
                    CurrentText.Size = (int.Parse(CurrentText.Size) - 10).ToString();
                    break;

                case "init":
                    CurrentText.Size = text.fontSize.ToString();
                    break;

                default:
                    CurrentText.Size = Size;
                    break;
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
            if (!cannotSkip)
            {
                SpeakTime = 0;
                while (state != State.WaitForInput) yield return null;
                SpeakTime = 1;
            }
        }

        #endregion

        #region Sound

        private void Play_ChatSE(Character character)
        {
            SEAudio.clip = character.ChatSE[UnityEngine.Random.Range(0, character.ChatSE.Length)];
            SEAudio.Play();
        }

        private void Play_CallSE(Character character, string SEname)
        {
            var FindSE 
                = Array.Find(character.CallSE, (SE) => SE.name == SEname);

            CallAudio.clip = FindSE;
            CallAudio.Play();
        }

        #endregion

        #region Speed

        private void Set_Speed(string speed)
        {
            switch (speed)
            {
                case "up":
                    SpeakTime -= 0.25f;
                    if (SpeakTime <= 0) SpeakTime = 0.001f;
                    break;

                case "down":
                    SpeakTime += 0.25f;
                    break;

                case "init":
                    SpeakTime = 1;
                    break;

                default:
                    SpeakTime = float.Parse(speed);
                    break;
            }

            LastSpeakTime = SpeakTime;
        }

        #endregion
    }
}