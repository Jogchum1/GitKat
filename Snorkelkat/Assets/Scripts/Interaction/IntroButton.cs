using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class IntroButton : MonoBehaviour
{
    bool isWrongButton;
    IntroTask introTask;

    public void AddEvent(IntroTask setIntroTask, bool isWrong)
    {
        isWrongButton = isWrong;
        introTask = setIntroTask;

    }

    public void Button_clicked()
    {
        if (isWrongButton)
        {
            introTask.PressedWrongButton();
        }
        else
        {
            introTask.PressedRightButton();
        }
    }
}
