using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class TestMessage : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Example;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("/size:up/Hi, /size:init/my name is Li.", "Li"));

        dialogTexts.Add(new DialogData("I am Sa. Popped out to let you know Asset can show other characters.", "Sa"));
        
        dialogTexts.Add(new DialogData("This Asset, The D'Dialog System has many features.", "Li"));

        dialogTexts.Add(new DialogData("You can easily change text /color:red/color, /color:white/and /size:up//size:up/size/size:init/ like this.", "Li", () => Show_Example(0)));

        dialogTexts.Add(new DialogData("Just put the command in the string!", "Li", () => Show_Example(1)));

        dialogTexts.Add(new DialogData("You can also change the character's sprite /emote:Sad/like this, /click//emote:Happy/Smile.", "Li",  () => Show_Example(2)));

        dialogTexts.Add(new DialogData("If you need an emphasis effect, /wait:0.5/wait... /click/or click command.", "Li", () => Show_Example(3)));

        dialogTexts.Add(new DialogData("Text can be /speed:down/slow... /speed:init//speed:up/or fast.", "Li", () => Show_Example(4)));

        dialogTexts.Add(new DialogData("You don't even need to click on the window like this.../speed:0.1/ tada!/close/", "Li", () => Show_Example(5)));

        dialogTexts.Add(new DialogData("/speed:0.1/AND YOU CAN'T SKIP THIS SENTENCE.", "Li", () => Show_Example(6), false));

        dialogTexts.Add(new DialogData("And here we go, the haha sound! /click//sound:haha/haha.", "Li", null, false));

        dialogTexts.Add(new DialogData("That's it! Please check the documents. Good luck to you.", "Sa"));

        DialogManager.Show(dialogTexts);
    }

    private void Show_Example(int index)
    {
        Example[index].SetActive(true);
    }
}
