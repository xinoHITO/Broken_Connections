using UnityEngine;

public class ReadLipsManager : MonoBehaviour
{
    public bool IsReadingLips { get; set; }
    public float AwkwardLevel = 0;
    public float AwkwardMax = 100;
    public float AwkwardGainSpeed = 5;
    public float AwkwardLoseSpeed = 1.5f;

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
    }
}
