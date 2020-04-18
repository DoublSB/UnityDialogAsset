using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Events;

namespace Doublsb.Dialog
{
    #region Enum
    public enum State
    {
        Active,
        Wait,
        Deactivate
    }

    public enum Command
    {
        print,
        color,
        emote,
        size,
        sound,
        speed,
        click,
        close,
        wait
    }

    public enum TextColor
    {
        aqua,
        black,
        blue,
        brown,
        cyan,
        darkblue,
        fuchsia,
        green,
        grey,
        lightblue,
        lime,
        magenta,
        maroon,
        navy,
        olive,
        orange,
        purple,
        red,
        silver,
        teal,
        white,
        yellow
    }
    #endregion

    #region Emotion
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
    #endregion

    /// <summary>
    /// Convert string to Data. Contains List of DialogCommand and DialogFormat.
    /// </summary>
    public class DialogData
    {
        //================================================
        //Public Variable
        //================================================
        public List<DialogCommand> Commands;
        public DialogFormat Format;

        public string PrintText = string.Empty;

        public bool isSkipable = true;
        public UnityAction Callback = null;

        //================================================
        //Public Method
        //================================================
        public DialogData(string originalString, bool isSkipable = true, UnityAction callback = null)
        {
            _init();
            _convert(originalString);

            this.isSkipable = isSkipable;
            this.Callback = callback;
        }

        public DialogData(string originalString, UnityAction callback)
        {
            _init();
            _convert(originalString);

            this.Callback = callback;
        }

        //================================================
        //Private Method
        //================================================
        private void _init()
        {
            string PrintText = string.Empty;

            Format = new DialogFormat();
            Commands = new List<DialogCommand>();
        }

        private void _convert(string originalString)
        {
            string printText = string.Empty;

            for (int i = 0; i < originalString.Length; i++)
            {
                if (originalString[i] != '/') printText += originalString[i];

                else // If find '/'
                {
                    // Convert last printText to command
                    if(printText != string.Empty)
                    {
                        Commands.Add(new DialogCommand(Command.print, printText));
                        printText = string.Empty;
                    }

                    // Substring /CommandSyntex/
                    var nextSlashIndex = originalString.IndexOf('/', i + 1);
                    string commandSyntex = originalString.Substring(i + 1, nextSlashIndex - i - 1);

                    // Add converted command
                    var com = _convert_Syntex_To_Command(commandSyntex);
                    if (com != null) Commands.Add(com);

                    // Move i
                    i = nextSlashIndex;
                }
            }

            if (printText != string.Empty) Commands.Add(new DialogCommand(Command.print, printText));
        }

        private DialogCommand _convert_Syntex_To_Command(string text)
        {
            var spliter = text.Split(':');

            Command command;
            if (Enum.TryParse(spliter[0], out command))
            {
                if (spliter.Length >= 2) return new DialogCommand(command, spliter[1]);
                else return new DialogCommand(command);
            }
            else
                Debug.LogError("Cannot parse to commands");

            return null;
        }
    }

    /// <summary>
    /// You can get RichText tagger of size and color.
    /// </summary>
    public class DialogFormat
    {
        //================================================
        //Private Variable
        //================================================
        private string _defaultSize = "60";
        private string _defaultColor = "white";

        private string _color;
        private string _size;


        //================================================
        //Public Method
        //================================================
        public DialogFormat(string defaultSize = "", string defaultColor = "")
        {
            _color = string.Empty;
            _size = string.Empty;

            if (defaultSize != string.Empty) _defaultSize = defaultSize;
            if (defaultColor != string.Empty) _defaultColor = defaultColor;
        }

        public string Color
        {
            set
            {
                if (isColorValid(value))
                {
                    _color = value;
                    if (_size == string.Empty) _size = _defaultSize; //한 쪽이 Valid면 다른 한 쪽도 Valid해야 한다
                }
            }

            get => _color;
        }

        public string Size
        {
            set
            {
                if (isSizeValid(value))
                {
                    _size = value;
                    if (_color == string.Empty) _color = _defaultColor; //한 쪽이 Valid면 다른 한 쪽도 Valid해야 한다
                }
            }

            get => _size;
        }

        public string OpenTagger
        {
            get
            {
                if (isValid) return $"<color={Color}><size={Size}>";
                else return string.Empty;
            }
        }

        public string CloseTagger
        {
            get
            {
                if (isValid) return "</size></color>";
                else return string.Empty;
            }
        }

        public void Resize(string command)
        {
            if (_size == string.Empty) Size = _defaultSize;

            switch (command)
            {
                case "up":
                    _size = (int.Parse(_size) + 10).ToString();
                    break;

                case "down":
                    _size = (int.Parse(_size) - 10).ToString();
                    break;

                case "init":
                    _size = _defaultSize;
                    break;

                default:
                    _size = command;
                    break;
            }
        }

        //================================================
        //Private Method
        //================================================
        private bool isValid
        {
            get => _color != string.Empty && _size != string.Empty;
        }

        private bool isColorValid(string Color)
        {
            TextColor textColor;
            Regex hexColor = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$");

            return Enum.TryParse(Color, out textColor) || hexColor.Match(Color).Success;
        }

        private bool isSizeValid(string Size)
        {
            float size;
            return float.TryParse(Size, out size);
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