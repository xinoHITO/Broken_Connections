using UnityEngine;
using UnityEngine.Events;

public class ReadLipsManager : MonoBehaviour
{
    public bool IsReadingLips { get; set; }
    public float AwkwardLevel = 0;
    public float AwkwardMax = 100;
    public float AwkwardGainSpeed = 5;
    public float AwkwardLoseSpeed = 1.5f;
    public UnityEvent OnStartReadingLips;
    public UnityEvent OnEndReadingLips;
    public UnityAction OnAwkwardMax;

    private void Update()
    {
        bool clicked = Input.GetMouseButtonDown(0);
        if (clicked)
        {
            IsReadingLips = !IsReadingLips;
        }
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
            OnEndReadingLips?.Invoke();
        }

        if (clicked)
        {
            if (IsReadingLips)
            {
                OnStartReadingLips?.Invoke();
            }
            else {
                OnEndReadingLips?.Invoke();
            }
        }
        
    }
}
