using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSensitivity : MonoBehaviour
{
    [SerializeField] bool _changeLookSpeed;

    [SerializeField] MouseLook playerCam, craneCam;
    [SerializeField] Vector2Int _switchLookValues = new Vector2Int(50, 500);
    [SerializeField] Vector2Int _switchJointValues = new Vector2Int(80, 800);

    DragJointedObject playerDrag => playerCam.GetComponent<DragJointedObject>();

    private void Start()
    {
        if (_changeLookSpeed)
        {
            playerCam.sensitivity.x = _switchLookValues.x;
            playerCam.sensitivity.y = _switchLookValues.x;

            craneCam.sensitivity.x = _switchLookValues.x;
            craneCam.sensitivity.y = _switchLookValues.x;

            playerDrag.jointStrength = _switchJointValues.x;
        }
        else
        {
            playerCam.sensitivity.x = _switchLookValues.y;
            playerCam.sensitivity.y = _switchLookValues.y;

            craneCam.sensitivity.x = _switchLookValues.y;
            craneCam.sensitivity.y = _switchLookValues.y;

            playerDrag.jointStrength = _switchJointValues.y;
        }
    }
}
