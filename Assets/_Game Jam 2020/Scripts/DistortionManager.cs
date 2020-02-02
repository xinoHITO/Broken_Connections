using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DistortionManager : MonoBehaviour
{
    public float DistortionTimeMin = 0.45f;

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

    public void ApplyDistortion(float dialogueLineDuration)
    {
        Debug.Log("APPLY DISTORTION:" + dialogueLineDuration);
        StartCoroutine(ApplyDistortionCoroutine(dialogueLineDuration));
    }

    IEnumerator ApplyDistortionCoroutine(float dialogueLineDuration)
    {
        float delayBeforeWarning = Random.Range(0, dialogueLineDuration - WarningDuration);
        yield return new WaitForSeconds(delayBeforeWarning);
        OnWarningStarted?.Invoke();
        yield return new WaitForSeconds(WarningDuration);
        OnWarningFinished?.Invoke();
        IsDistortionActive = true;
        float randomValue = Random.Range(DistortionTimeMin, 1.0f);
        float distortionDuration = (dialogueLineDuration - WarningDuration) * randomValue;
        string msg = string.Format("% value:{0} - Distortion duration:{1}", randomValue, distortionDuration);
        Debug.Log(msg);
        yield return new WaitForSeconds(distortionDuration);
        IsDistortionActive = false;
    }

}
