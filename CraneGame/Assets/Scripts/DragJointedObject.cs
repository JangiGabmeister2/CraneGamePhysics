using UnityEngine;

public class DragJointedObject : MonoBehaviour
{
    [SerializeField] Camera _cam;
    //what the player will drag in the scene AKA the knob
    Transform _selectedDragObject;
    //a game object which is a reference to the mouse position relative to the knob
    GameObject _mouseRef;
    //grabbable game objects are within this layer
    [SerializeField] LayerMask _leverLayer;
    //the strength at which the player drags knobs
    [SerializeField] float _jointStrength;

    private void Update()
    {
        //creates a raycast from the camera to its forward direction
        //returns transform of what's in front of camera == knob
        RaycastHit rayHit;
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out rayHit, Mathf.Infinity, _leverLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _selectedDragObject = rayHit.collider.gameObject.transform;
            }
        }

        //if a knob is being dragged
        if (_selectedDragObject != null)
        {
            if (_mouseRef == null)
            {
                _mouseRef = new GameObject("Ray Joint");
                _mouseRef.transform.parent = _selectedDragObject;
            }

            //creates a raycast from screen center to mouse position in world space
            //sets position of mouse ref game object to wherever the raycast collides with an object
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            _mouseRef.transform.position = ray.GetPoint(Vector3.Distance(_selectedDragObject.position, transform.position));
            _mouseRef.transform.rotation = _selectedDragObject.rotation;

            //refers to the configurable joint component on the knob
            ConfigurableJoint joint = _selectedDragObject.GetComponent<ConfigurableJoint>();

            //while dragging, if the mouse position gets further away from the knob, the stronger the force and the faster it reaches its max angle
            float delta = Mathf.Pow(Vector3.Distance(_mouseRef.transform.position, _selectedDragObject.position), 4);

            if (Mathf.Abs(_selectedDragObject.parent.forward.z) > 0.5f)
            {
                //if the joint rotates via y axis
                if (joint.angularYMotion != ConfigurableJointMotion.Locked)
                {
                    //sets target rotation to max angle limit
                    joint.targetRotation = Quaternion.Euler(0, joint.angularYLimit.limit, 0);
                    //rotates joint towards mouse position relative to knob position
                    if (_mouseRef.transform.position.x > _selectedDragObject.position.x)
                    {
                        joint.targetAngularVelocity = new Vector3(0, delta * -_jointStrength * Time.deltaTime);
                    }
                    else
                    {
                        joint.targetAngularVelocity = new Vector3(0, delta * _jointStrength * Time.deltaTime);
                    }
                }
                //if the joint rotates via x axis
                if (joint.angularXMotion != ConfigurableJointMotion.Locked)
                {
                    joint.targetRotation = Quaternion.Euler(joint.highAngularXLimit.limit, 0, 0);
                    if (_mouseRef.transform.position.y < _selectedDragObject.position.y)
                    {
                        joint.targetAngularVelocity = new Vector3(delta * -_jointStrength * Time.deltaTime, 0);
                    }
                    else
                    {
                        joint.targetAngularVelocity = new Vector3(delta * _jointStrength * Time.deltaTime, 0);
                    }
                }
            }

            //once the player lets go of the mouse button, it returns joint values to normal, and destroys mouse ref GO
            if (Input.GetMouseButtonUp(0))
            {
                _selectedDragObject = null;
                joint.targetRotation = Quaternion.identity;
                joint.targetAngularVelocity = Vector3.zero;
                Destroy(_mouseRef);
            }
        }
    }
}
