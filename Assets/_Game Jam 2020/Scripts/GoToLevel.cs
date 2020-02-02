using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    public string level = "gameplay";
    public float delay = 2;

    public bool activateAtStart = false;

    private void Start()
    {
        if (activateAtStart)
        {
            ChangeLevel();
        }
    }
    public void ChangeLevel()
    {
        StartCoroutine(ChangeLevelCoroutine());
    }

    IEnumerator ChangeLevelCoroutine()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(level);
    }
}
