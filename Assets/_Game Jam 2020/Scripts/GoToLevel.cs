using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    public string level = "gameplay";
    public float delay = 2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeLevel());
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(level);
    }
}
