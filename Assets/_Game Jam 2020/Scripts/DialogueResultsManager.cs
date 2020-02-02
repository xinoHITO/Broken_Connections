using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueResultsManager : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject ResultCanvas;
    public GameObject GoodEndingLabel;
    public GameObject BadEndingLabel;
    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.onAllDialoguesFinished += OnConversationEnded;
    }

    private void OnConversationEnded(int correctAnswers, int questionsCount)
    {
        MainCanvas.SetActive(false);
        ResultCanvas.SetActive(true);
        if (correctAnswers > 0)
        {
            GoodEndingLabel.SetActive(true);
            BadEndingLabel.SetActive(false);
        }
        else {
            GoodEndingLabel.SetActive(false);
            BadEndingLabel.SetActive(true);
        }
    }
}
