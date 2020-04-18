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
        [Header("Game Objects")]
        public GameObject Printer;
        public GameObject Characters;

        [Header("UI Objects")]
        public Text Printer_Text;

        [Header("Audio Objects")]
        public AudioSource SEAudio;
        public AudioSource CallAudio;

        [Header("Preference")]
        public float Delay = 0.1f;

        [HideInInspector]
        public State state;

        //================================================
        //Private Method
        //================================================
        private Character _current_Character;
        private DialogData _current_Data;

        private float _currentDelay;
        private float _lastDelay;
        private UnityAction _callback;
        private Coroutine _textingRoutine;

        //================================================
        //Public Method
        //================================================
        #region Show & Hide
        public void Show(DialogData Data, Character character = null)
        {
            _current_Data = Data;
            _current_Character = character;

            _textingRoutine = StartCoroutine(Activate());
        }

        public void Show(List<DialogData> Data, Character character = null)
        {
            _current_Character = character;

            StartCoroutine(Activate_List(Data));
        }

        public void Click_Window()
        {
            switch (state)
            {
                case State.Active:
                    StartCoroutine(_skip()); break;

                case State.Wait:
                    Hide(); break;
            }
        }

        public void Hide()
        {
            StopCoroutine(_textingRoutine);

            Printer.SetActive(false);
            Characters.SetActive(false);

            if (_callback != null) _callback.Invoke();
            else state = State.Deactivate;
        }
        #endregion

        #region Sound

        public void Play_ChatSE(Character character)
        {
            SEAudio.clip = character.ChatSE[UnityEngine.Random.Range(0, character.ChatSE.Length)];
            SEAudio.Play();
        }

        public void Play_CallSE(string SEname)
        {
            var FindSE
                = Array.Find(_current_Character.CallSE, (SE) => SE.name == SEname);

            CallAudio.clip = FindSE;
            CallAudio.Play();
        }

        #endregion

        #region Speed

        public void Set_Speed(string speed)
        {
            switch (speed)
            {
                case "up":
                    _currentDelay -= 0.25f;
                    if (_currentDelay <= 0) _currentDelay = 0.001f;
                    break;

                case "down":
                    _currentDelay += 0.25f;
                    break;

                case "init":
                    _currentDelay = Delay;
                    break;

                default:
                    _currentDelay = float.Parse(speed);
                    break;
            }

            _lastDelay = _currentDelay;
        }

        #endregion

        //================================================
        //Private Method
        //================================================
        private void _initialize()
        {
            _currentDelay = Delay;
            _lastDelay = 0.1f;
            Printer_Text.text = string.Empty;

            Printer.SetActive(true);
            Characters.SetActive(_current_Character != null);
        }

        #region Show Text

        private IEnumerator Activate_List(List<DialogData> DataList)
        {
            foreach (var Data in DataList)
            {
                Show(Data, _current_Character);

                while (state != State.Deactivate) { yield return null; }

                if (Data.Callback != null) Data.Callback.Invoke();
            }
        }

        private IEnumerator Activate()
        {
            _initialize();

            state = State.Active;

            foreach (var item in _current_Data.Commands)
            {
                switch (item.Command)
                {
                    case Command.print:
                        yield return StartCoroutine(_print(item.Context));
                        break;

                    case Command.color:
                        _current_Data.Format.Color = item.Context;
                        break;

                    case Command.emote:
                        _emote(item.Context);
                        break;

                    case Command.size:
                        _current_Data.Format.Resize(item.Context);
                        break;

                    case Command.sound:
                        Play_CallSE(item.Context);
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

            state = State.Wait;
        }

        private IEnumerator _waitInput()
        {
            while (!Input.GetMouseButtonDown(0)) yield return null;
            _currentDelay = _lastDelay;
        }

        private IEnumerator _print(string Text)
        {
            _current_Data.PrintText += _current_Data.Format.OpenTagger;

            for (int i = 0; i < Text.Length; i++)
            {
                _current_Data.PrintText += Text[i];
                Printer_Text.text = _current_Data.PrintText + _current_Data.Format.CloseTagger;

                if (Text[i] != ' ') Play_ChatSE(_current_Character);
                if (_currentDelay != 0) yield return new WaitForSeconds(_currentDelay);
            }

            _current_Data.PrintText += _current_Data.Format.CloseTagger;
        }

        public void _emote(string Text)
        {
            _current_Character.GetComponent<Image>().sprite = _current_Character.Emotion.Data[Text];
        }

        private IEnumerator _skip()
        {
            if (_current_Data.isSkipable)
            {
                _currentDelay = 0;
                while (state != State.Wait) yield return null;
                _currentDelay = Delay;
            }
        }

        #endregion

    }
}