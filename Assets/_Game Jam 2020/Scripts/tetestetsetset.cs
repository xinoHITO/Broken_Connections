using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetestetsetset : MonoBehaviour
{
    public CinemachineImpulseSource impulse;
    // Start is called before the first frame update
    void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
        impulse.GenerateImpulse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
