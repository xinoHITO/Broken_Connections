using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessManager : MonoBehaviour
{
    public float LerpSpeed = 5;
    private Volume volume;

    private float OriginValue;
    private float TargetValue;
    private float CurrentAlpha;
    
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentAlpha = CurrentAlpha + (Time.deltaTime * LerpSpeed);
        volume.weight = Mathf.Lerp(OriginValue, TargetValue, CurrentAlpha);
    }

    public void FadeIn() {
        SetTargetValue(1);
    }

    public void FadeOut()
    {
        SetTargetValue(0);
    }

    private void SetTargetValue(float targetValue) {
        TargetValue = targetValue;
        OriginValue = 1 - targetValue;
        CurrentAlpha = 0;
        
    }
}
