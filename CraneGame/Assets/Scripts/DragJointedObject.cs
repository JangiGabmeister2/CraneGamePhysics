using UnityEngine;

public class DragJointedObject : MonoBehaviour
{
    [SerializeField] Camera _cam;
    Transform _selectedDragObject;
    GameObject _dragObject;
    [SerializeField] LayerMask _leverLayer;

    private void Update()
    {
        //Raycast
        RaycastHit rayHit;
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out rayHit, Mathf.Infinity, _leverLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _selectedDragObject = rayHit.collider.gameObject.transform;
            }
        }

        if (_selectedDragObject != null)
        {
            HingeJoint joint = _selectedDragObject.GetComponentInParent<HingeJoint>();
            JointMotor motor = joint.motor;

            //Create drag point object for reference where players mouse is pointing
            if (_dragObject == null)
            {
                _dragObject = new GameObject("Ray Lever");
                _dragObject.transform.parent = _selectedDragObject;
            }

            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            _dragObject.transform.position = ray.GetPoint(Vector3.Distance(_selectedDragObject.position, transform.position));
            _dragObject.transform.rotation = _selectedDragObject.rotation;

            float delta = Mathf.Pow(Vector3.Distance(_dragObject.transform.position, _selectedDragObject.position), 3);

            //Applying velocity to lever motor
            float speedMultiplier = 60000;
            if (Mathf.Abs(_selectedDragObject.parent.forward.z) > 0.5f)
            {
                if (_dragObject.transform.position.y > _selectedDragObject.position.y)
                {
                    motor.targetVelocity = delta * -speedMultiplier * Time.deltaTime;
                }
                else
                {
                    motor.targetVelocity = delta * speedMultiplier * Time.deltaTime;
                }
            }

            joint.motor = motor;

            if (Input.GetMouseButtonUp(0))
            {
                _selectedDragObject = null;
                motor.targetVelocity = 0;
                joint.motor = motor;
                Destroy(_dragObject);
            }
        }
    }
}
