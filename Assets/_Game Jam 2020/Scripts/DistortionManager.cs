using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DistortionManager : MonoBehaviour
{
    public float DistortionTimeMin = 0.75f;
    public float DistortionTimeMax = 2.0f;

    public float WarningDuration = 1.0f;
    public bool IsDistortionActive { get; set; }

    public UnityAction OnWarningStarted;
    public UnityAction OnWarningFinished;


    private void OnDisable()
    {
        if (IsDistortionActive)
        {
            OnWarningFinished?.Invoke();
            IsDistortionActive = false;
        }
        StopAllCoroutines();
    }

    public void ApplyDistortion(float dialogueLineDuration = 1) {
        StartCoroutine(ApplyDistortionCoroutine(dialogueLineDuration));
    }

    IEnumerator ApplyDistortionCoroutine(float dialogueLineDuration) {

        Debug.Log("APPLY DISTORTION");
        OnWarningStarted?.Invoke();
        yield return new WaitForSeconds(WarningDuration);
        OnWarningFinished?.Invoke();
        IsDistortionActive = true;
        float distortionDuration = (dialogueLineDuration - WarningDuration) * Random.Range(0.0f, 1.0f);
        Debug.Log(distortionDuration);
        yield return new WaitForSeconds(distortionDuration);
        IsDistortionActive = false;
    }

}
