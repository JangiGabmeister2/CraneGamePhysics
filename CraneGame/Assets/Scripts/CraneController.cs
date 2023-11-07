using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    public ConfigurableJoint crane, elevationLever, trainLever, rotationWheel;

    public int jointValues1, jointValues2, jointValues3;

    private void Update()
    {
        jointValues3 = (int)(rotationWheel.transform.localRotation.y * 90);
    }
}