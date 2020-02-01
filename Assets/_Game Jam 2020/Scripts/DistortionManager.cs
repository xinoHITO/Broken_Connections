using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DistortionManager : MonoBehaviour
{
    private float LastDistortionTime;
    public bool IsDistortionActive { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        LastDistortionTime = Random.Range(0.0f, 2.0f);
        StartCoroutine(ApplyDistortion());
    }

    IEnumerator ApplyDistortion() {
        while (true)
        {
            IsDistortionActive = true;
            yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
            IsDistortionActive = false;
            yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        }
    }
}
