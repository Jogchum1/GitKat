using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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

    // Start is called before the first frame update
    void Start()
    {

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

        if (alpha >= 1)
        {
            if (!invoked)
            {
                endOfIntroEvent.Invoke();
                invoked = true;
            }
        }
    }

}
