using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject LoseCanvas;

    public float WaitBeforeGoingToLoseScene = 2.0f;
    public string LoseScene = "lose";
    private ReadLipsManager readLips;


    // Start is called before the first frame update
    void Start()
    {
        readLips = FindObjectOfType<ReadLipsManager>();
        readLips.OnAwkwardMax += LoseGame;
    }

    void LoseGame()
    {
        MainCanvas.SetActive(false);
        LoseCanvas.SetActive(true);
        StartCoroutine(GoToLoseScene());
    }

    IEnumerator GoToLoseScene() {
        yield return new WaitForSeconds(WaitBeforeGoingToLoseScene);
        SceneManager.LoadScene(LoseScene);
    }
}
