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

    [Header("Data")]
    public DialogueData Dialogue;
    public float SpeakRate = 0.1f;
    public float PauseBetweenLines = 2.0f;

    private DistortionManager Distortion;
    private ReadLipsManager ReadLips;
    private int DialogueIndex;
    private int LineIndex;
    private int LetterIndex;

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
            
            string newLetter = "" + Dialogue.dialogues[0].lines[LineIndex][LetterIndex];
            
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
            if (LetterIndex == Dialogue.dialogues[0].lines[LineIndex].Length)
            {
                yield return new WaitForSeconds(PauseBetweenLines);
                
                LineIndex++;
                if (LineIndex >= Dialogue.dialogues[0].lines.Length)
                {
                    break;
                }
                LineLabel.text = "";
                LetterIndex = 0;
            }
        }
        StartCoroutine(ShowQuestion());
    }

    IEnumerator ShowQuestion() {
        QuestionBox.SetActive(true);
        LineBox.SetActive(false);

        QuestionLabel.text = Dialogue.dialogues[DialogueIndex].question.text;
        Text[] answerTexts = AnswerLabelsContainer.GetComponentsInChildren<Text>();
        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = Dialogue.dialogues[DialogueIndex].question.anwers[i];
        }
        DialogueIndex++;
        yield return null;
    }
}
