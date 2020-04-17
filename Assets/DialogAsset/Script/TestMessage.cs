using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class TestMessage : MonoBehaviour
{
    public DialogManager DialogManager;
    public Character Character;

    private void Awake()
    {
        var dialogTexts = new List<string>();

        dialogTexts.Add("/size:up/안녕. /size:down/내 이름은 /color:red/리/color:white/이다.");
        dialogTexts.Add("/color:white//size:80//color:green/이 구문에 오류가 있었지?");
        dialogTexts.Add("하지만 해결했지.");

        DialogManager.Show(dialogTexts, Character);
    }
}
