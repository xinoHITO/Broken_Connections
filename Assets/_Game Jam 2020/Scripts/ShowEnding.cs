using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnding : MonoBehaviour
{
    public Text endingLabel;
    // Start is called before the first frame update
    void Start()
    {
        endingLabel.text = PlayerPrefs.GetString("BrokenConnections.Ending");
    }

}
