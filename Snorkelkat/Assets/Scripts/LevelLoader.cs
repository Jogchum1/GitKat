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
        SceneManager.LoadScene(levelToLoad);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
