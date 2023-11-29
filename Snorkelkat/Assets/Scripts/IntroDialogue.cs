using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System;
using TMPro;

public class IntroDialogue : DialogueViewBase
{
    public List<LocalizedLine> lines = new List<LocalizedLine>();

    public List<TMP_Text> textObjects = new List<TMP_Text>();
    [SerializeField] CanvasGroup canvasGroup;

    [SerializeField] private float appearanceTime = 0.5f;

    [SerializeField] private float disappearanceTime = 0.5f;

    [SerializeField] TMPro.TextMeshProUGUI text;

    [SerializeField] RectTransform container;

    [SerializeField] private bool waitForInput;

    Coroutine currentAnimation;

    Action advanceHandler = null;

    private float Scale
    {
        set => container.localScale = new Vector3(value, value, value);
    }

    public void Start()
    {
        Scale = 0;
        canvasGroup.alpha = 0;
    }

    
    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (gameObject.activeInHierarchy == false)
        {
            onDialogueLineFinished();
            return;
        }

        Debug.Log($"{this.name} running line {dialogueLine.TextID}");
        canvasGroup.alpha = 1;
        Scale = 0;

        advanceHandler = requestInterrupt;

        currentAnimation = this.Tween(
            0f, 1f,
            appearanceTime,
            (from, to, t) => Scale = Mathf.Lerp(from, to, t),
            () =>
            {
                Debug.Log($"{this.name} finished presenting {dialogueLine.TextID}");
                currentAnimation = null;

                if (waitForInput)
                {
                    advanceHandler = requestInterrupt;
                }
                else
                {
                    
                    advanceHandler = null;
                    if (lines.Count < 5)
                    {
                        lines.Add(dialogueLine);
                        Debug.Log("Add lines");
                    }
                    else
                    {
                        lines.RemoveAt(0);
                        Debug.Log(lines.Count + " is dit dan 1 minder?");
                        Debug.Log("Sort lines");
                        SortLines();
                        lines.Add(dialogueLine);
                    }
                    Debug.Log(lines.Count + " is lines count");


                    UpdateTextBoxes();

                    onDialogueLineFinished();
                }
            }
            );
    }

    public void UpdateTextBoxes()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            string tmp = lines[i].Text.Text.ToString();
            textObjects[i].text = tmp;
            Debug.Log(i + " is i van update");
        }
    }

    public void SortLines()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            string tmp = lines[i].Text.Text.ToString();
            textObjects[i].text = tmp;
            Debug.Log(i + " is i van sort");
        }
    }

    public override void DialogueComplete()
    {
        canvasGroup.alpha = 0;
        lines.Clear();
        for (int i = 0; i < textObjects.Count; i++)
        {
            textObjects[i].text = null;
        }
    }
    public void test2()
    {
        Debug.Log("Test2");
    }

    
    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (gameObject.activeInHierarchy == false)
        {
            
            onDialogueLineFinished();
            return;
        }

        
        advanceHandler = null;

        Debug.Log($"{this.name} was interrupted while presenting {dialogueLine.TextID}");

        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
            currentAnimation = null;
        }

        Scale = 1f;

        onDialogueLineFinished();
    }

    
    public override void DismissLine(Action onDismissalComplete)
    {
        if (gameObject.activeInHierarchy == false)
        {
            onDismissalComplete();
            return;
        }

        Debug.Log($"{this.name} dismissing line");

        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
            currentAnimation = null;
        }

        
        advanceHandler = () =>
        {
            if (currentAnimation != null)
            {
                StopCoroutine(currentAnimation);
                currentAnimation = null;
            }
            advanceHandler = null;
            onDismissalComplete();
            Scale = 0f;
        };

        currentAnimation = this.Tween(
            1f, 0f,
            disappearanceTime,
            (from, to, t) => Scale = Mathf.Lerp(from, to, t),
            () =>
            {
                advanceHandler = null;
                Debug.Log($"{this.name} finished dismissing line");
                currentAnimation = null;
                onDismissalComplete();
            });
    }

    
    public override void UserRequestedViewAdvancement()
    {
        
        advanceHandler?.Invoke();
    }
}


