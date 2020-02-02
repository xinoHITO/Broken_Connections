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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ApplyDistortion());
    }

    IEnumerator ApplyDistortion() {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(DistortionTimeMin, DistortionTimeMax));

            OnWarningStarted?.Invoke();
            yield return new WaitForSeconds(WarningDuration);
            OnWarningFinished?.Invoke();
            IsDistortionActive = true;
            yield return new WaitForSeconds(Random.Range(DistortionTimeMin, DistortionTimeMax));
            IsDistortionActive = false;
            yield return new WaitForSeconds(Random.Range(DistortionTimeMin, DistortionTimeMax));
        }
    }
}
