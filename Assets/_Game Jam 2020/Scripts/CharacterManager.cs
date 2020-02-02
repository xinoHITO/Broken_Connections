using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    private Animator animator;
    private DialogueManager dialogueManager;
    private ReadLipsManager readLipsManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.OnLineStarted += OnLineStarted;
        dialogueManager.OnLineFinished+= OnFinishedLine;
        dialogueManager.OnRightAnswer += OnRightAnswer;
        dialogueManager.OnWrongAnswer += OnWrongAnswer;

        readLipsManager = FindObjectOfType<ReadLipsManager>();
        readLipsManager.OnAwkwardMax += OnAwkwardReachMax;
    }

    private void OnAwkwardReachMax()
    {
        animator.SetTrigger("Angry");
    }

    private void OnLineStarted()
    {
        animator.SetTrigger("Talk");
    }

    private void OnFinishedLine()
    {
        animator.SetTrigger("Idle");
    }

    private void OnWrongAnswer()
    {
        animator.SetTrigger("Angry");
    }

    private void OnRightAnswer()
    {
        animator.SetTrigger("Idle");
    }

}
