using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSensitivity : MonoBehaviour
{
    [SerializeField] bool _changeLookSpeed;

    [SerializeField] MouseLook playerCam, craneCam;
    DragJointedObject playerDrag => playerCam.GetComponent<DragJointedObject>();

    private void Start()
    {
        if (_changeLookSpeed)
        {
            playerCam.sensitivity.x = 50;
            playerCam.sensitivity.y = 50;

            craneCam.sensitivity.x = 50;
            craneCam.sensitivity.y = 50;

            playerDrag.jointStrength = 80;
        }
        else
        {
            playerCam.sensitivity.x = 500;
            playerCam.sensitivity.y = 500;

            craneCam.sensitivity.x = 500;
            craneCam.sensitivity.y = 500;

            playerDrag.jointStrength = 800;
        }
    }
}
