using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doublsb.Dialog
{
    //[Editable] You can freely change emotion list.
    //It responds to character script either.
    public enum Emotion
    {
        Normal,
        Happy,
        Sad,
        Blushful,
        Cynical,
        Fearful,
        Excited,
        Surprised
    }

    public enum State
    {
        Show,
        Wait,
        Hide
    }
}