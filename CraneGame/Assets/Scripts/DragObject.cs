using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public LayerMask leverLayer;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayHit, 10, leverLayer))
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 lookPosition = rayHit.point - transform.position;
                lookPosition.x = 0;
                Quaternion lookRotation = Quaternion.LookRotation(lookPosition);
                transform.rotation = lookRotation; 
            }
        }
    }
}