using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public float forceAmmount = 5;
    
    Rigidbody _dragObject;
    Vector3 _offset;
    
    Vector3 _orginalPosition;
    float _selectionDistance;
    
    public GameObject marker;
    public LayerMask grabLayer;
    
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        //if player clicks on an object, if that object is moveable, the player can drag that object around the world space
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
    
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, grabLayer))
            {
                _selectionDistance = Vector3.Distance(ray.origin, hit.point);
                _offset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _selectionDistance));
                _orginalPosition = hit.collider.transform.position;
            }
        }
    
        if (Input.GetMouseButtonUp(0))
        {
            _dragObject = null;
        }
    
    }
    
    private void FixedUpdate()
    {
        if (_dragObject)
        {
            marker.transform.position = Input.mousePosition + _offset;
    
            Vector3 mousePositionOffset = Camera.main.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, _selectionDistance)) - _orginalPosition;
    
            _dragObject.velocity = (_orginalPosition + mousePositionOffset - _dragObject.transform.position) * forceAmmount * Time.deltaTime;
        }
    }
}
