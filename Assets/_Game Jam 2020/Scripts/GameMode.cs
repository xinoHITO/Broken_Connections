using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public float WaitBeforeGoingToEndingScene = 5.0f;
    public string EndingScene = "ending";
    private ReadLipsManager readLips;
    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        readLips = FindObjectOfType<ReadLipsManager>();
        readLips.OnAwkwardMax += EndGame;

    }

    void EndGame()
    {
        StartCoroutine(GoToLoseScene());
    }

    IEnumerator GoToLoseScene() {
        yield return new WaitForSeconds(WaitBeforeGoingToEndingScene);
        SceneManager.LoadScene(EndingScene);
    }
}
