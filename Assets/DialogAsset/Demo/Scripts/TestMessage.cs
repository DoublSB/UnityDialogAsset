using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class TestMessage : MonoBehaviour
{
    public DialogManager DialogManager;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("/size:up/안녕. /size:down/내 이름은 /color:red/리/color:white/이다.", "Li"));
        dialogTexts.Add(new DialogData("/color:white//size:80//color:green/이 구문에 오류가 있었지?", "Li"));
        dialogTexts.Add(new DialogData("하지만 해결했지.", "Li"));

        dialogTexts.Add(new DialogData("I am Sa.", "Sa"));

        DialogManager.Show(dialogTexts);
    }
}
