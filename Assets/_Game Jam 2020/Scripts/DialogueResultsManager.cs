using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueResultsManager : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject ResultCanvas;
    public GameObject GoodEndingLabel;
    public GameObject NeutralEndingLabel;
    public GameObject BadEndingLabel;
    public GameObject AwkwardEndingLabel;

    public float WaitBeforeGoingToEndingScene = 5.0f;
    public string EndingScene = "ending";


    private DialogueManager dialogueManager;
    private ReadLipsManager readLipsManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.onAllDialoguesFinished += OnConversationEnded;
        readLipsManager = FindObjectOfType<ReadLipsManager>();
        readLipsManager.OnAwkwardMax += OnAwkwardReachMax;
    }

    private void ShowEnding(GameObject endingLabel,string ending) {
        MainCanvas.SetActive(false);
        ResultCanvas.SetActive(true);

        GoodEndingLabel.SetActive(false);
        BadEndingLabel.SetActive(false);
        NeutralEndingLabel.SetActive(false);
        AwkwardEndingLabel.SetActive(false);

        endingLabel.SetActive(true);
        PlayerPrefs.SetString("BrokenConnections.Ending", ending);
        StartCoroutine(GoToEndingScene());
    }


    private void OnAwkwardReachMax()
    {
        ShowEnding(AwkwardEndingLabel,"Awkward ending");
    }

    private void OnConversationEnded(int correctAnswers, int questionsCount)
    {
    
        if (correctAnswers >= questionsCount)
        {
            ShowEnding(GoodEndingLabel,"Good ending");
        }
        else if (correctAnswers < questionsCount && correctAnswers > 1)
        {
            ShowEnding(NeutralEndingLabel,"Neutral ending");
        }
        else
        {
            ShowEnding(BadEndingLabel,"Bad ending");
        }
    }


    IEnumerator GoToEndingScene()
    {
        yield return new WaitForSeconds(WaitBeforeGoingToEndingScene);
        SceneManager.LoadScene(EndingScene);
    }
}
