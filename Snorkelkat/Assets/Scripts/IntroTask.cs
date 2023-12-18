using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroTask : MonoBehaviour
{
    public Slider timer;
    public float timerSpeed = 1;
    public float speedIncrease = 0.1f;
    public List<GameObject> questionList = new List<GameObject>();

    private int currentQuestion = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject question in questionList)
        {
            question.SetActive(false);
        }
        questionList[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer.value = timer.value - Time.deltaTime * timerSpeed;
        if(timer.value <= 0)
        {
            PressedWrongButton();
        }
    }

    public void PressedRightButton()
    {
        timer.value = 1;
        timerSpeed += speedIncrease;
        questionList[currentQuestion].SetActive(false);
        currentQuestion++;
        if (currentQuestion < questionList.Count)
        {
            questionList[currentQuestion].SetActive(true);
        }
        else
        {
            Debug.Log("Wake up in woestijn");
        }
    }

    public void PressedWrongButton()
    {
        timer.value = 1;
    }
}
