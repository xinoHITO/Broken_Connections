using UnityEngine;
using UnityEngine.Events;

public class ReadLipsManager : MonoBehaviour
{
    public bool IsReadingLips { get; set; }
    public float AwkwardLevel = 0;
    public float AwkwardMax = 100;
    public float AwkwardGainSpeed = 5;
    public float AwkwardLoseSpeed = 1.5f;
    public UnityAction OnStartReadingLips;
    public UnityAction OnEndReadingLips;
    public UnityAction OnAwkwardMax;

    private void Update()
    {
        IsReadingLips = Input.GetMouseButton(0);
        if (IsReadingLips)
        {
            AwkwardLevel += Time.deltaTime * AwkwardGainSpeed;
        }
        else {
            AwkwardLevel -= Time.deltaTime * AwkwardLoseSpeed;
        }
        AwkwardLevel = Mathf.Max(0, AwkwardLevel);

        if (AwkwardLevel >= AwkwardMax)
        {
            this.enabled = false;
            OnAwkwardMax?.Invoke();
        }


        if (Input.GetMouseButtonDown(0))
        {
            OnStartReadingLips?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnEndReadingLips?.Invoke();
        }
    }
}
