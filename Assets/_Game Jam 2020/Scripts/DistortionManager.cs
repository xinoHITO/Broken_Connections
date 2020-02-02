using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DistortionManager : MonoBehaviour
{
    public float DistortionTimeMin = 0.45f;
    public float DistortionTimeMax = 1.2f;

    public float WarningDuration = 1.0f;
    public bool IsDistortionActive { get; set; }

    public UnityEvent OnWarningStarted;
    public UnityEvent OnWarningFinished;

    public UnityEvent OnDistortionStarted;
    public UnityEvent OnDistortionFinished;

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
        float distDuration = Random.Range(DistortionTimeMin, dialogueLineDuration - WarningDuration);
        distDuration = Mathf.Min(DistortionTimeMax, distDuration);
        float delayBefore = dialogueLineDuration - WarningDuration - distDuration;

        yield return new WaitForSeconds(delayBefore);
        OnWarningStarted?.Invoke();
        yield return new WaitForSeconds(WarningDuration);
        OnWarningFinished?.Invoke();
        IsDistortionActive = true;
        OnDistortionStarted?.Invoke();
        float randomValue = Random.Range(0.0f, 1.0f);
        string msg = string.Format("% value:{0} - Distortion duration:{1}", randomValue, distDuration);
        Debug.Log(msg);
        yield return new WaitForSeconds(distDuration);
        IsDistortionActive = false;
        OnDistortionFinished?.Invoke();
    }

}
