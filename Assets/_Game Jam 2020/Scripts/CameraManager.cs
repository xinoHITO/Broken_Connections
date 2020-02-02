using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Animator animator;
    private ReadLipsManager readLips;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        readLips = FindObjectOfType<ReadLipsManager>();
        readLips.OnStartReadingLips.AddListener(OnStartReadingLips);
        readLips.OnEndReadingLips.AddListener(OnEndReadingLips);
        readLips.OnAwkwardMax += LoseGame;
    }

    private void OnStartReadingLips()
    {
        animator.SetTrigger("Zoom");
    }

    private void OnEndReadingLips()
    {
        animator.SetTrigger("Normal");
    }

    private void LoseGame()
    {
        animator.SetTrigger("Normal");
        readLips.OnStartReadingLips.RemoveAllListeners();
        readLips.OnEndReadingLips.RemoveAllListeners();
    }
}
