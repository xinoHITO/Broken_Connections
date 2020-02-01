using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAwkwardLevel : MonoBehaviour
{
    public Image bar;
    private ReadLipsManager ReadLips;
    // Start is called before the first frame update
    void Start()
    {
        ReadLips = FindObjectOfType<ReadLipsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = ReadLips.AwkwardLevel / ReadLips.AwkwardMax;
    }
}
