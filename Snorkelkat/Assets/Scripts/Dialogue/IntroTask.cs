using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class IntroTask : MonoBehaviour
{
    [SerializeField]
    private Image backGround;

    public float alpha = 0;
    public Slider timer;
    public float timerSpeed = 1;
    public float speedIncrease = 0.1f;
    public List<GameObject> questionList = new List<GameObject>();
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private UnityEvent endOfIntroEvent;
    private bool invoked= false;
    private int currentQuestion = 0;
    private bool introStarted;

    void Update()
    {
        if (introStarted)
        {
            timer.value = timer.value - Time.deltaTime * timerSpeed;
            if (timer.value <= 0)
            {
                PressedWrongButton();
            }
        }
    }

    [YarnCommand("StartIntro")]
    public void StartIntro()
    {
        introStarted = true;
    }

    public void PressedRightButton()
    {
        timer.value = 1;
        timerSpeed += speedIncrease;

        questionList[currentQuestion].SetActive(false);

        if (currentQuestion < questionList.Count)
        {
            currentQuestion++;
            questionList[currentQuestion].SetActive(true);
        }
        else
        {
            if (!invoked)
            {
                endOfIntroEvent.Invoke();
                invoked= true;
            }
        }
    }

    public void PressedWrongButton()
    {
        alpha += 0.03f;
        backGround.color = new Color(backGround.color.r, backGround.color.g, backGround.color.b, alpha);
        timer.value = 1;
        timerSpeed += speedIncrease;
        Debug.Log(currentQuestion);
        if (currentQuestion < questionList.Count)
        {
            questionList[currentQuestion].SetActive(false);
            currentQuestion++;
            if(currentQuestion != questionList.Count)
                questionList[currentQuestion].SetActive(true);
        }
        else
        {
            if (!invoked)
            {
                endOfIntroEvent.Invoke();
                invoked = true;
            }
        }
    }

}
