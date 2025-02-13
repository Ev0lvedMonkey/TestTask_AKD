using UnityEngine;
using Zenject;

public class ObjectPicker : MonoBehaviour
{
    private HoldPoint _holdPoint;
    private Rigidbody _pickedObjectRigBody;
    private bool _isPicked;

    private void Update()
    {
        if (!_isPicked || _pickedObjectRigBody == null)
            return;
        MovePickedObject();
    }

    private void MovePickedObject()
    {
        _pickedObjectRigBody.transform.position = _holdPoint.transform.position;
    }

    public void PickUp(PickableObject pickedObject)
    {
        Rigidbody pickedObjectRb = pickedObject.GetComponent<Rigidbody>();
        _pickedObjectRigBody = pickedObjectRb;
        _pickedObjectRigBody.useGravity = false;
        _pickedObjectRigBody.isKinematic = false;
        _isPicked = true;
    }

    public void Drop()
    {
        if (_pickedObjectRigBody != null)
        {
            _pickedObjectRigBody.useGravity = true;
            _pickedObjectRigBody.velocity = Vector3.zero;
            _pickedObjectRigBody = null;
        }
        _isPicked = false;
    }

    public void UpdateHoldPointPositionY(float newY)
    {
        Vector3 newPosition = _holdPoint.transform.position;
        newPosition.y = newY;
        _holdPoint.transform.position = newPosition;
    }

    [Inject]
    private void Construct(HoldPoint holdPoint)
    {
        _holdPoint = holdPoint;
    }
}
