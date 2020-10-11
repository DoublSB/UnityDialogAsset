using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class TestMessage_Selection : MonoBehaviour
{
    public DialogManager DialogManager;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        var Text1 = new DialogData("What is 2 times 5?");
        Text1.SelectList.Add("Correct", "10");
        Text1.SelectList.Add("Wrong", "7");
        Text1.SelectList.Add("Whatever", "Why should I care?");

        Text1.Callback = () => Check_Correct();

        dialogTexts.Add(Text1);

        DialogManager.Show(dialogTexts);
    }

    private void Check_Correct()
    {
        if(DialogManager.Result == "Correct")
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData("You are right."));

            DialogManager.Show(dialogTexts);
        }
        else if (DialogManager.Result == "Wrong")
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData("You are wrong."));

            DialogManager.Show(dialogTexts);
        }
        else
        {
            var dialogTexts = new List<DialogData>();

            dialogTexts.Add(new DialogData("Right. You don't have to get the answer."));

            DialogManager.Show(dialogTexts);
        }
    }
}
