using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

    public class DialogText
    {
        public List<DialogCommand> Commands;
        public List<Command> CloseCommands;

        public int Size;

        public string BackText
        {
            get
            {
                string result = string.Empty;

                for (int i = CloseCommands.Count - 1; i >= 0; i--)
                {
                    switch (CloseCommands[i])
                    {
                        case Command.color:
                            result += "</color>";
                            break;
                        case Command.size:
                            result += "</size>";
                            break;
                    }
                }

                return result;
            }
        }

        public string PrintText;

        private string _originalText;
        
        public DialogText(string OriginalText, int size)
        {
            _originalText = OriginalText;

            CloseCommands = new List<Command>();
            PrintText = string.Empty;

            Size = size;
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