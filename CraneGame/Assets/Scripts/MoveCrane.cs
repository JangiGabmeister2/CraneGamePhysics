using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrane : MonoBehaviour
{
    [SerializeField] ConfigurableJoint crane;
    [SerializeField] GameObject trainGO;
    [SerializeField] float rotationSpeed, elevationSpeed, trainMovementSpeed;

    private CraneController Controller => GetComponent<CraneController>();
    private float WheelJointStrength => Controller.wheelStrength;
    private float ElevatorJointStrength => Controller.elevatorStrength;
    private float TrainJointStrength => Controller.trainStrength;

    private void Update()
    {
        crane.targetAngularVelocity = new Vector3(0, rotationSpeed * WheelJointStrength, 0);

        crane.targetVelocity = new Vector3(0, -elevationSpeed * ElevatorJointStrength, 0);

        trainGO.transform.localPosition += new Vector3(0, -trainMovementSpeed * TrainJointStrength, 0);
        //Mathf.Clamp(trainGO.transform.position.y, -40, 40);
    }
}
