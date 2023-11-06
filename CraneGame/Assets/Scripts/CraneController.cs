using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    public ConfigurableJoint crane, elevationLever, trainLever, rotationWheel;

    public float jointValues1, jointValues2, jointValues3;

    private void Start()
    {
        Mathf.Clamp(jointValues1, 0, 90);
        Mathf.Clamp(jointValues2, 0, 90);
        Mathf.Clamp(jointValues3, 145, -145);
    }

    private void Update()
    {

    }
}
