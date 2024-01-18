using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Loading")]
    [SerializeField] private int levelToLoad;
    [SerializeField] private Slider slider;

    private void Start()
    {
        canvasGroup.alpha = 1;
        animator.SetTrigger("Enter");
    }

    public void LoadLevel()
    {
        animator.SetTrigger("Exit");

        StartCoroutine(LoadLevelASync(levelToLoad));
    }

    IEnumerator LoadLevelASync(int levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            slider.value = progressValue;
            yield return null;
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
