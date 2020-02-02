using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] MusicSources;
    private float[] TargetVolumes;

    private DialogueManager dialogueManager;
    private ReadLipsManager readLipsManager;
    private int MusicIndex = 1;
    private float LastTimeMusicWasSet;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.OnRightAnswer += OnRightAnswer;
        dialogueManager.OnWrongAnswer += OnWrongAnswer;

        readLipsManager = FindObjectOfType<ReadLipsManager>();
        readLipsManager.OnAwkwardMax += OnAwkwardReachesMax;

        TargetVolumes = new float[MusicSources.Length];
        ChangeMusic(MusicIndex);
    }

    private void OnAwkwardReachesMax()
    {
        ChangeMusic(0);
    }

    private void Update()
    {
        for (int i = 0; i < MusicSources.Length; i++)
        {
            float t = Mathf.Min(Time.timeSinceLevelLoad - LastTimeMusicWasSet,1);
            MusicSources[i].volume = Mathf.Lerp(MusicSources[i].volume, TargetVolumes[i], t);
        }
    }

    private void OnWrongAnswer()
    {
        MusicIndex--;
        if (MusicIndex < 0)
        {
            MusicIndex = 0;
        }
        ChangeMusic(MusicIndex);
    }

    private void OnRightAnswer()
    {
        MusicIndex++;
        if (MusicIndex > MusicSources.Length-1)
        {
            MusicIndex = MusicSources.Length - 1;
        }
        ChangeMusic(MusicIndex);
    }

    private void ChangeMusic(int musicIndex) {
        for (int i = 0; i < TargetVolumes.Length; i++)
        {
            TargetVolumes[i] = 0;
        }
        TargetVolumes[musicIndex] = 1;
        LastTimeMusicWasSet = Time.timeSinceLevelLoad;
    }

}
