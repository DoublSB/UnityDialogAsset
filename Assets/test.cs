using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class test : MonoBehaviour
{
    public DialogManager dialogManager;

    void Start()
    {
        DialogData dialogData = new DialogData("/sound:haha/haha.", "Li");

        dialogData.Format.DefaultSize = "80";

        dialogManager.Show(dialogData);
    }
}
