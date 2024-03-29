using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public List<GameObject> gameObjectsToEnable;
    [SerializeField]
    public Door goalDoor;
    [SerializeField]
    private Vector3 spawnpoint;
    [SerializeField]
    private float transTime;

    [HideInInspector]
    public Vector3 goalPos;
    private Image transScreen;
    private GameManager gameManager;
    private CamManager camManager;

    private void Start()
    {
        camManager = CamManager.instance;
        goalPos = transform.position + spawnpoint;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transScreen = GameObject.Find("BlackScreen").GetComponent<Image>();

        if (gameObjectsToEnable == null)
        {
            Debug.LogError("No room assigned in " + gameObject.name);
        }
    }
    public void EnterDoor(Collider2D collider)
    {
        StartCoroutine(EnteringDoor(collider));
    }

    private IEnumerator EnteringDoor(Collider2D playerCol)
    {
        gameManager.TogglePlayerMovement();

        float duration = transTime / 3;
        yield return TransitionScreen(Color.clear, Color.black, duration);

        gameManager.StopPlayerVelocity();
        playerCol.gameObject.transform.position = goalDoor.goalPos;
        camManager.currentCamera.ForceCameraPosition(goalDoor.goalPos, Quaternion.identity);

        foreach (GameObject gameObject in gameObjectsToEnable)
        {
            gameObject.SetActive(false);
        }

        foreach (GameObject gameObject1 in goalDoor.gameObjectsToEnable)
        {
            gameObject1.SetActive(true);
        }

        yield return new WaitForSeconds(transTime/3);

        yield return TransitionScreen(Color.black, Color.clear, duration);

        gameManager.TogglePlayerMovement();
    }

    public IEnumerator TransitionScreen(Color start, Color end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            transScreen.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        transScreen.color = end;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, goalDoor.transform.position);
        Gizmos.DrawCube(transform.position + spawnpoint, Vector3.one);
        Gizmos.DrawCube(transform.position + spawnpoint, Vector3.one);
    }

}
