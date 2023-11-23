using UnityEngine;

public class CraneController : MonoBehaviour
{
    [SerializeField, Tooltip("Wheel knob")] GameObject wheel;
    [SerializeField, Tooltip("X = Down angle\nY = Current angle\nZ = Up angle")] Vector3 wheelValues;
    [Tooltip("Distance of current angle to max angle as a percentage.")] public float wheelStrength;

    [SerializeField, Space(20), Tooltip("Elevator lever knob")] public GameObject elevatorLever;
    [SerializeField, Tooltip("X = Down angle\nY = Current angle\nZ = Up angle")] Vector3 elevationValues;
    [Tooltip("Distance of current angle to max angle as a percentage.")] public float elevatorStrength;

    [SerializeField, Space(20), Tooltip("Train Lever knob")] public GameObject trainLever;
    [SerializeField, Tooltip("X = Down angle\nY = Current angle\nZ = Up angle")] Vector3 trainValues;
    [Tooltip("Distance of current angle to max angle as a percentage.")] public float trainStrength;

    private ConfigurableJoint joint1 => wheel.GetComponent<ConfigurableJoint>();
    private ConfigurableJoint joint2 => elevatorLever.GetComponent<ConfigurableJoint>();
    private ConfigurableJoint joint3 => trainLever.GetComponent<ConfigurableJoint>();
    private float wheelJointLimit => joint1.angularYLimit.limit;
    private float elevatorJointLowLimit => joint2.lowAngularXLimit.limit;
    private float elevatorJointHighLimit => joint2.highAngularXLimit.limit;
    private float trainJointLowLimit => joint3.lowAngularXLimit.limit;
    private float trainJointHighLimit => joint3.highAngularXLimit.limit;

    private void Update()
    {
        //displays the connected joint's low and high angular limits via x and z axes respectively,
        //and displays the joint's current angle between them via y axis.
        wheelValues.x = 360 - wheelJointLimit; //-145
        wheelValues.y = Mathf.Abs(wheel.transform.localEulerAngles.y - 360);
        wheelValues.z = wheelJointLimit; //145
        //displays how close the y value is to the other axes percentage-wise.
        if (wheelValues.y <= 360 && wheelValues.y >= wheelValues.x)
        {
            wheelStrength = GetValueBasedOnCloseness(360, wheelValues.x, wheelValues.y);
        }
        else if (wheelValues.y <= wheelValues.z && wheelValues.y >= 0)
        {
            wheelStrength = -GetValueBasedOnCloseness(0, wheelValues.z, wheelValues.y);
        }

        elevationValues.x = -elevatorJointLowLimit; //45
        elevationValues.y = Mathf.Abs(elevatorLever.transform.localEulerAngles.x - 360);
        elevationValues.z = 360 - elevatorJointHighLimit; //315
        if (elevationValues.y <= elevationValues.x && elevationValues.y >= 0)
        {
            elevatorStrength = GetValueBasedOnCloseness(0, elevationValues.x, elevationValues.y);
        }
        else if (elevationValues.y >= elevationValues.z && elevationValues.y <= 360)
        {
            elevatorStrength = -GetValueBasedOnCloseness(360, elevationValues.z, elevationValues.y);
        }

        trainValues.x = -trainJointLowLimit; //45
        trainValues.y = Mathf.Abs(trainLever.transform.localEulerAngles.x - 360);
        trainValues.z = 360 - trainJointHighLimit; //315
        if (trainValues.y <= trainValues.x && trainValues.y >= 0)
        {
            trainStrength = GetValueBasedOnCloseness(0, trainValues.x, trainValues.y);
        }
        else if (trainValues.y >= trainValues.z && trainValues.y <= 360)
        {
            trainStrength = -GetValueBasedOnCloseness(360, trainValues.z, trainValues.y);
        }
    }

    private float GetValueBasedOnCloseness(float lowValue, float highValue, float valueToCompare)
    {
        //finds where the value stands between the low value and a high value
        //returns that position as a float between 0 and 1, where 0 is low, and 1 is high.
        float lerp = Mathf.InverseLerp(lowValue, highValue, valueToCompare);

        //then calculates how close that float is from both high and low values
        float result = Mathf.Lerp(0, 100, lerp);

        //returning float can be implied as percentage
        return result;
    }
}