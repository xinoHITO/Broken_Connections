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
        float distDuration = Random.Range(DistortionTimeMin, dialogueLineDuration - WarningDuration);
        float delayBefore = dialogueLineDuration - WarningDuration - distDuration;

        yield return new WaitForSeconds(delayBefore);
        OnWarningStarted?.Invoke();
        yield return new WaitForSeconds(WarningDuration);
        OnWarningFinished?.Invoke();
        IsDistortionActive = true;
        float randomValue = Random.Range(0.0f, 1.0f);
        string msg = string.Format("% value:{0} - Distortion duration:{1}", randomValue, distDuration);
        Debug.Log(msg);
        yield return new WaitForSeconds(distDuration);
        IsDistortionActive = false;
    }

}
