using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextReader : MonoBehaviour
{
    public Text Label;
    public string TextToShow="Text to show";
    public float ShowRate = 0.08f;

    private int letterIndex; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowNextLetter());
    }

    IEnumerator ShowNextLetter() {
        Label.text = "";
        while (true)
        {
            Label.text += TextToShow[letterIndex];
            yield return new WaitForSeconds(ShowRate);
            letterIndex++;
            if (letterIndex >= TextToShow.Length)
            {
                break;
            }
        }
    }
}

