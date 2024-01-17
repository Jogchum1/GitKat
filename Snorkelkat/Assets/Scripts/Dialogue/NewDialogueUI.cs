using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Yarn;
using Yarn.Unity;
using TMPro;

public class BartDialogueUI : DialogueViewBase
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI currentLine;
    [SerializeField] private List<TMP_Text> historyTextObjects = new List<TMP_Text>();
    private List<string> linesSaid = new List<string>();

    [SerializeField] private GameObject mouseIcon;

    [SerializeField] private OptionView optionViewPrefab;
    [SerializeField] private Transform optionViewParent;
    private List<OptionView> optionViews = new List<OptionView>();
    Action<int> OnOptionSelected;

    Action advanceHandler = null;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UserRequestedViewAdvancement();
        }
    }

    public override void DialogueStarted()
    {
        canvasGroup.alpha = 1;
        linesSaid.Clear();
        for (int i = 0; i < historyTextObjects.Count; i++)
        {
            historyTextObjects[i].text = null;
        }
    }

    public override void DialogueComplete()
    {
        canvasGroup.alpha = 0;
        linesSaid.Clear();
        for (int i = 0; i < historyTextObjects.Count; i++)
        {
            historyTextObjects[i].text = null;
        }
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        //dismiss if dialogueview not active
        if (gameObject.activeInHierarchy == false)
        {
            onDialogueLineFinished();
            return;
        }

        mouseIcon.SetActive(true);

        advanceHandler = requestInterrupt;

        currentLine.text = dialogueLine.Text.Text;
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        mouseIcon.SetActive(false);
        onDialogueLineFinished?.Invoke();
    }

    public override void DismissLine(Action onDismissalComplete)
    {
        mouseIcon.SetActive(false);
        AddLineToHistory(currentLine.text);
        currentLine.text = "";
        onDismissalComplete?.Invoke();
    }

    public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
    {
        advanceHandler = null;

        // If we don't already have enough option views, create more
        while (dialogueOptions.Length > optionViews.Count)
        {
            var optionView = CreateNewOptionView();
            optionView.gameObject.SetActive(false);
        }

        // Set up all of the option views
        int optionViewsCreated = 0;

        for (int i = 0; i < dialogueOptions.Length; i++)
        {
            var optionView = optionViews[i];
            var option = dialogueOptions[i];

            if (option.IsAvailable == false)
            {
                // Don't show this option.
                continue;
            }

            optionView.gameObject.SetActive(true);

            optionView.Option = option;

            // The first available option is selected by default
            if (optionViewsCreated == 0)
            {
                optionView.Select();
            }

            optionViewsCreated += 1;
        }

        // Note the delegate to call when an option is selected
        OnOptionSelected = onOptionSelected;

        OptionView CreateNewOptionView()
        {
            var optionView = Instantiate(optionViewPrefab);
            optionView.transform.SetParent(optionViewParent, false);
            optionView.transform.SetAsLastSibling();

            optionView.OnOptionSelected = OptionViewWasSelected;
            optionViews.Add(optionView);

            return optionView;
        }

        void OptionViewWasSelected(DialogueOption option)
        {
            StartCoroutine(OptionViewWasSelectedInternal(option));

            IEnumerator OptionViewWasSelectedInternal(DialogueOption selectedOption)
            {
                DisableOptionView();
                AddLineToHistory(selectedOption.Line.RawText);
                OnOptionSelected(selectedOption.DialogueOptionID);
                yield return null;
            }
        }
    }
    private void DisableOptionView()
    {
        // Hide all existing option views
        foreach (var optionView in optionViews)
        {
            optionView.gameObject.SetActive(false);
        }
    }

    public void AddLineToHistory(String dialogueLine)
    {
        if (linesSaid.Count < historyTextObjects.Count)
        {
            linesSaid.Add(dialogueLine);
            DisplayLinesReversed();
        }
        else
        {
            linesSaid.Add(dialogueLine);
            linesSaid.RemoveAt(0);
            DisplayLines();
        }
    }

    public void DisplayLines()
    {
        foreach (string line in linesSaid)
        {
            historyTextObjects[linesSaid.IndexOf(line)].text = line;
        }
    }
    
    public void DisplayLinesReversed()
    {
        historyTextObjects.Reverse();
        linesSaid.Reverse();
        DisplayLines();
        historyTextObjects.Reverse();
        linesSaid.Reverse();
    }

    public override void UserRequestedViewAdvancement()
    {
        if (advanceHandler != null)
        {
            advanceHandler?.Invoke();
        }
    }
}
