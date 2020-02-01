using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public float SpeakRate = 0.1f;
    public float PauseBetweenLines = 2.0f;

    private DistortionManager Distortion;
    private ReadLipsManager ReadLips;
    private int DialogueIndex;
    private int LineIndex;
    private int LetterIndex;

    private int CorrectAnwersCount;

    // Start is called before the first frame update
    void Start()
    {
        Distortion = FindObjectOfType<DistortionManager>();
        ReadLips = FindObjectOfType<ReadLipsManager>();
        StartCoroutine(ShowDialogueLines());
    }

    IEnumerator ShowDialogueLines()
    {
        QuestionBox.SetActive(false);
        LineBox.SetActive(true);
        LetterIndex = 0;
        LineIndex = 0;
        LineLabel.text = "";
        while (true)
        {
            yield return new WaitForSeconds(SpeakRate);
            DialogueData.Dialogue currentDialogue = Dialogue.dialogues[DialogueIndex];
            string newLetter = "" + currentDialogue.lines[LineIndex][LetterIndex];
            
            if (Distortion.IsDistortionActive && newLetter != " ")
            {
                if (ReadLips.IsReadingLips)
                {
                    newLetter = "<color=red>" + newLetter + "</color>";
                }
                else {
                    newLetter = "*";
                }
            }
            LineLabel.text += newLetter;
            LetterIndex++;
            if (LetterIndex == currentDialogue.lines[LineIndex].Length)
            {
                yield return new WaitForSeconds(PauseBetweenLines);
                
                LineIndex++;
                if (LineIndex >= currentDialogue.lines.Length)
                {
                    break;
                }
                LineLabel.text = "";
                LetterIndex = 0;
            }
        }
        ShowQuestion();
    }

    void ShowQuestion() {
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
            else {
                answerButtons[i].onClick.AddListener(() => OnClickCorrectAnswer());
            }
            
        }
    }

    private void OnClickWrongAnswer() {
        Debug.Log("INCORRECT");
        WrongAnswer.SetActive(true);
        RightAnswer.SetActive(false);
        StartCoroutine(EndQuestion());
    }

    private void OnClickCorrectAnswer() {
        Debug.Log("CORRECT");
        CorrectAnwersCount++;
        RightAnswer.SetActive(true);
        WrongAnswer.SetActive(false);
        StartCoroutine(EndQuestion());
    }

    private IEnumerator EndQuestion() {

        QuestionBox.SetActive(false);
        LineBox.SetActive(true);
        ResultBox.SetActive(true);
        DialogueIndex++;
        if (DialogueIndex >= Dialogue.dialogues.Length)
        {
            Debug.Log("ENDED DIALOGUE - YOU GOT "+CorrectAnwersCount+" OUT OF "+Dialogue.dialogues.Length);
        }
        else {
            yield return new WaitForSeconds(PauseBetweenLines);
            ResultBox.SetActive(false);
            StartCoroutine(ShowDialogueLines());
        }
        yield return null;

    }
}
