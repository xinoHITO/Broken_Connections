using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Lines")]
    public GameObject LineBox;
    public Text LineLabel;
    [Header("Question")]
    public GameObject QuestionBox;
    public Text QuestionLabel;
    public RectTransform AnswerLabelsContainer;
    [Header("Result")]
    public GameObject ResultBox;
    public GameObject RightAnswer;
    public GameObject WrongAnswer;

    [Header("Data")]
    public DialogueData Dialogue;
    public AudioClip textAudioClip;
    public float SpeakRate = 0.1f;
    public float PauseBetweenLines = 2.0f;

    public float BlinkDuration = 0.2f;

    private DistortionManager Distortion;
    private ReadLipsManager ReadLips;
    private int DialogueIndex;
    private int LineIndex;
    private int LetterIndex;

    private int CorrectAnwersCount;
    private string CurrentUnalteredLine;
    private string CurrentLine;
    private bool HasBlinkingStarted;
    private int BlinkingStartIndex;
    private bool IsBlinkCurrentlyWhite;

    private AudioSource textAudioSource;

    public UnityAction OnRightAnswer;
    public UnityAction OnWrongAnswer;

    public UnityEvent OnFinishedDialogue;

    // Start is called before the first frame update
    void Start()
    {
        Distortion = FindObjectOfType<DistortionManager>();
        Distortion.OnWarningStarted += BlinkingStarts;
        Distortion.OnWarningFinished += BlinkingFinishes;

        ReadLips = FindObjectOfType<ReadLipsManager>();
        ReadLips.OnAwkwardMax += OnAwkwardReachesMax;
        textAudioSource = GetComponent<AudioSource>();

        StartCoroutine(ShowDialogueLines());

    }

    private void OnAwkwardReachesMax()
    {
        textAudioSource.Stop();
        StopAllCoroutines();
    }

    private void Update()
    {
        ApplyBlinkingToText();
        LineLabel.text = CurrentLine;
    }

    IEnumerator ShowDialogueLines()
    {
        QuestionBox.SetActive(false);
        LineBox.SetActive(true);
        LetterIndex = 0;
        LineIndex = 0;
        LineLabel.text = "";
        CurrentUnalteredLine = "";
        CurrentLine = "";

        DialogueData.Dialogue currentDialogue = Dialogue.dialogues[DialogueIndex];
        Distortion.ApplyDistortion(currentDialogue.lines[LineIndex].Length * SpeakRate);
        textAudioSource.Play();
        while (true)
        {
            
            yield return new WaitForSeconds(SpeakRate);

            string newLetter = "" + currentDialogue.lines[LineIndex][LetterIndex];
            CurrentUnalteredLine += newLetter;

            if (Distortion.IsDistortionActive && newLetter != " ")
            {
                if (ReadLips.IsReadingLips)
                {
                    newLetter = "<color=red>" + newLetter + "</color>";
                }
                else
                {
                    newLetter = "*";
                }
            }
            CurrentLine += newLetter;
            LetterIndex++;
            if (LetterIndex == currentDialogue.lines[LineIndex].Length)
            {

                BlinkingFinishes();
                textAudioSource.Stop();

                yield return new WaitForSeconds(PauseBetweenLines);

                LineIndex++;
                if (LineIndex >= currentDialogue.lines.Length)
                {
                    break;
                }
                CurrentUnalteredLine = "";
                CurrentLine = "";
                LineLabel.text = "";
                LetterIndex = 0;
                textAudioSource.Play();
                Distortion.ApplyDistortion(currentDialogue.lines[LineIndex].Length * SpeakRate);

            }
        }
        ShowQuestion();
    }

    #region BLINKING

    private void BlinkingStarts()
    {
        HasBlinkingStarted = true;
        BlinkingStartIndex = CurrentUnalteredLine.Length;
        StartCoroutine(ChangeBlinkColor());
        Debug.Log("blinking start");
    }

    private void BlinkingFinishes()
    {
        HasBlinkingStarted = false;
        IsBlinkCurrentlyWhite = true;
        BlinkingStartIndex = -1;
        Debug.Log("blinking end");
    }

    private IEnumerator ChangeBlinkColor()
    {
        while (true)
        {
            IsBlinkCurrentlyWhite = false;
            yield return new WaitForSeconds(BlinkDuration);
            IsBlinkCurrentlyWhite = true;
            if (!HasBlinkingStarted)
            {
                break;
            }
            yield return new WaitForSeconds(BlinkDuration);
        }
    }

    private void ApplyBlinkingToText()
    {
        if (HasBlinkingStarted)
        {
            string nonBlinkingPart = CurrentUnalteredLine.Substring(0, BlinkingStartIndex);
            if (BlinkingStartIndex < CurrentUnalteredLine.Length)
            {
                string blinkingPart = CurrentUnalteredLine.Substring(BlinkingStartIndex, CurrentUnalteredLine.Length - BlinkingStartIndex);
                if (IsBlinkCurrentlyWhite)
                {
                    CurrentLine = nonBlinkingPart + "<color=white>" + blinkingPart + "</color>";
                }
                else
                {
                    CurrentLine = nonBlinkingPart + "<color=#405b90>" + blinkingPart + " </color>";
                }
            }
            else
            {
                CurrentLine = nonBlinkingPart;
            }
        }
    }

    #endregion

    #region QUESTION_FUNCTIONS

    void ShowQuestion()
    {
        QuestionBox.SetActive(true);
        LineBox.SetActive(false);

        DialogueData.Question currentQuestion = Dialogue.dialogues[DialogueIndex].question;
        QuestionLabel.text = currentQuestion.text;
        Text[] answerTexts = AnswerLabelsContainer.GetComponentsInChildren<Text>();
        Button[] answerButtons = AnswerLabelsContainer.GetComponentsInChildren<Button>();
        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = currentQuestion.anwers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            if (i != currentQuestion.correctAnswerIndex)
            {
                answerButtons[i].onClick.AddListener(() => OnClickWrongAnswer());
            }
            else
            {
                answerButtons[i].onClick.AddListener(() => OnClickCorrectAnswer());
            }

        }
    }

    private void OnClickWrongAnswer()
    {
        Debug.Log("INCORRECT");
        WrongAnswer.SetActive(true);
        RightAnswer.SetActive(false);
        OnWrongAnswer?.Invoke();
        StartCoroutine(EndQuestion());
    }

    private void OnClickCorrectAnswer()
    {
        Debug.Log("CORRECT");
        CorrectAnwersCount++;
        RightAnswer.SetActive(true);
        WrongAnswer.SetActive(false);
        OnRightAnswer?.Invoke();
        StartCoroutine(EndQuestion());
    }

    private IEnumerator EndQuestion()
    {

        QuestionBox.SetActive(false);
        LineBox.SetActive(true);
        ResultBox.SetActive(true);
        DialogueIndex++;
        if (DialogueIndex >= Dialogue.dialogues.Length)
        {
            Debug.Log("ENDED DIALOGUE - YOU GOT " + CorrectAnwersCount + " OUT OF " + Dialogue.dialogues.Length);
        }
        else
        {
            yield return new WaitForSeconds(PauseBetweenLines);
            ResultBox.SetActive(false);
            StartCoroutine(ShowDialogueLines());
        }
        yield return null;

    }

    #endregion
}
