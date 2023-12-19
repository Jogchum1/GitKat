using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class IntroTask : MonoBehaviour
{
    [SerializeField]
    private GameObject wrongButtonPrefab;
    [SerializeField]
    private GameObject rightButtonPrefab;
    [SerializeField]
    private Vector3 leftPoint;
    [SerializeField]
    private Vector3 rightPoint;
    [SerializeField]
    private float offset;
    [SerializeField]
    private float padding;
    [SerializeField]
    private Image backGround;

    public int numberOfButtons = 5;
    public float alpha = 0;
    public Slider timer;
    public float timerSpeed = 1;
    public float speedIncrease = 0.1f;
    public List<GameObject> questionList = new List<GameObject>();
    private List<Vector3> randomPositions = new List<Vector3>();
    private List<GameObject> buttons = new List<GameObject>();
    private int level;
    public GameObject newButtonObject;
    public IntroButton newButton;

    private int currentQuestion = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnButtons(5);
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
        foreach (GameObject buttonGameObject in buttons)
        {
            Destroy(buttonGameObject);
        }
        buttons.Clear();
        SpawnButtons(numberOfButtons);

        //questionList[currentQuestion].SetActive(false);
        //currentQuestion++;
        //if (currentQuestion < questionList.Count)
        //{
        //    questionList[currentQuestion].SetActive(true);
        //}
        //else
        //{
        //    Debug.Log("Wake up in woestijn");
        //}
    }

    public void PressedWrongButton()
    {
        alpha += 0.1f;
        numberOfButtons += 1;
        backGround.color = new Color(backGround.color.r, backGround.color.g, backGround.color.b, alpha);
        timer.value = 1;
        foreach (GameObject buttonGameObject in buttons)
        {
            Destroy(buttonGameObject);
        }
        buttons.Clear();
        SpawnButtons(numberOfButtons);
        if(alpha >= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void SpawnButtons(int amountWrongButtons)
    {
        newButtonObject = Instantiate(rightButtonPrefab, NewRandomPos(), Quaternion.identity, gameObject.transform);
        buttons.Add(newButtonObject);
        newButton = newButtonObject.GetComponent<IntroButton>();
        newButton.AddEvent(this, false);

        for (int i = 0; i < amountWrongButtons; i++)
        {
            newButtonObject = Instantiate(wrongButtonPrefab, NewRandomPos(), Quaternion.identity, gameObject.transform);
            buttons.Add(newButtonObject);
            newButton = newButtonObject.GetComponent<IntroButton>();
            newButton.AddEvent(this, true);
        }

        randomPositions.Clear();
    }

    private Vector3 NewRandomPos()
    {
        Vector3 newPos = RandomPos();
        if (randomPositions.Count > 0)
        {
            foreach (Vector3 vector in randomPositions)
            {
                if (newPos.x < vector.x + padding && newPos.x > vector.x - padding && newPos.y < vector.y + padding && newPos.y > vector.y - padding)
                {
                    level++;
                    if (level >= 50)
                    {
                        return Vector3.zero;
                    }
                    return NewRandomPos();
                }
            }
        }
        randomPositions.Add(newPos);
        level = 0;
        return newPos;
    }

    private Vector3 RandomPos()
    {

        return new Vector3(UnityEngine.Random.Range(leftPoint.x + offset, rightPoint.x - offset), UnityEngine.Random.Range(leftPoint.y + offset, rightPoint.y - offset), 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(gameObject.transform.position, 1);
        //top line
        Gizmos.DrawLine(leftPoint, new Vector3(rightPoint.x, leftPoint.y, leftPoint.z));
        //bottom line
        Gizmos.DrawLine(new Vector3(leftPoint.x, rightPoint.y, leftPoint.z), rightPoint);
        //left line
        Gizmos.DrawLine(leftPoint, new Vector3(leftPoint.x, rightPoint.y, leftPoint.z));
        //right line
        Gizmos.DrawLine(rightPoint, new Vector3(rightPoint.x, leftPoint.y, leftPoint.z));
    }
}
