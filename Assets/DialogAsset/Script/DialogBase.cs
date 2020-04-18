using System.Collections.Generic;
using UnityEngine;
using System;

namespace Doublsb.Dialog
{
    public enum State
    {
        Show,
        Texting,
        WaitForInput,
        Wait,
        Hide
    }

    public enum Command
    {
        text,
        color,
        emote,
        size,
        sound,
        speed,
        click,
        close,
        wait
    }

    [Serializable]
    public class Emotion
    {
        //================================================
        //Public Variable
        //================================================
        private Dictionary<string, Sprite> _data;
        public Dictionary<string, Sprite> Data
        {
            get
            {
                if (_data == null) _init_emotionList();
                return _data;
            }
        }

        public string[] _emotion;
        public Sprite[] _sprite;

        //================================================
        //Private Method
        //================================================
        private void _init_emotionList()
        {
            _data = new Dictionary<string, Sprite>();

            if (_emotion.Length != _sprite.Length)
                Debug.LogError("Emotion and Sprite have different lengths");

            for (int i = 0; i < _emotion.Length; i++)
                _data.Add(_emotion[i], _sprite[i]);
        }
    }

    public class DialogText
    {
        public List<DialogCommand> Commands;

        public string Color;
        public string Size;

        public string OpenTagger { get => $"<color={Color}><size={Size}>"; }
        public readonly string CloseTagger = "</size></color>";

        public bool hasOpenTagger;

        public string PrintText;

        private string _originalText;

        public DialogText(string OriginalText = "")
        {
            _originalText = OriginalText;
            PrintText = string.Empty;

            Color = string.Empty;
            Size = string.Empty;
            hasOpenTagger = false;

            _convertToCommand();
        }

        private void _convertToCommand()
        {
            Commands = new List<DialogCommand>();
            string identifier = string.Empty;

            for (int i = 0; i < _originalText.Length; i++)
            {
                if (_originalText[i] != '/')
                {
                    identifier += _originalText[i];
                }

                else
                {
                    Commands.Add(new DialogCommand(Command.text, identifier));

                    identifier = string.Empty;

                    int closeIndex = _originalText.IndexOf('/', i + 1);
                    string commandSyntex = _originalText.Substring(i + 1, closeIndex - i - 1);
                    var com = _convertToCommand(commandSyntex);

                    if (com != null) Commands.Add(com);

                    i = closeIndex;
                }
            }

            if(identifier != string.Empty) Commands.Add(new DialogCommand(Command.text, identifier));
        }

        private DialogCommand _convertToCommand(string text)
        {
            var spliter = text.Split(':');

            Command command;
            if (Enum.TryParse(spliter[0], out command))
            {
                if(spliter.Length >= 2) return new DialogCommand(command, spliter[1]);
                else return new DialogCommand(command);
            }
            else
                Debug.LogError("Cannot parse to commands");

            return null;
        }
    }

    public class DialogCommand
    {
        public Command Command;
        public string Context;

        public DialogCommand(Command command, string context = "")
        {
            Command = command;
            Context = context;
        }
    }
}