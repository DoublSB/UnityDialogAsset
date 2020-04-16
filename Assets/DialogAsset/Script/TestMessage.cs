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
        dialogTexts.Add("나는 랩을 할 수 있지. /speed:0.02/속사포랩이나가신다나는아무튼트랩을하고있다 /speed:0.1/Ya /wait:0.1/ Ya.");
        dialogTexts.Add("봤나? 내 솜씨는 정말이지, 아주 아름답/close/다고 할 수 있다.");
        dialogTexts.Add("/emote:Sad/내 말. 자르지 말아줄래.");
        dialogTexts.Add("/emote:Normal/니가 누르기 전까지 말하지 않을 것이다. /click//emote:Happy/옳지.");
        dialogTexts.Add("아주 고오오오오맙다.");

        DialogManager.Show(dialogTexts, Character);
    }
}
