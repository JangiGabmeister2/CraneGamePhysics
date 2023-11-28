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
        //rotation
        crane.targetAngularVelocity = new Vector3(0, rotationSpeed * WheelJointStrength / 1000, 0);

        //elevation
        crane.targetVelocity = new Vector3(0, -elevationSpeed * ElevatorJointStrength, 0);

        //train
        var localPosition = trainGO.transform.localPosition;
        localPosition += new Vector3(0, -trainMovementSpeed * TrainJointStrength / 50000, 0);
        
        Vector3 pos = localPosition;
        pos.y = Mathf.Clamp(localPosition.y, -0.5f, 16.5f);
        localPosition = pos;
        trainGO.transform.localPosition = localPosition;
    }
}