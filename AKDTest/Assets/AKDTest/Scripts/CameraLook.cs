using UnityEngine;
using Zenject;

public class CameraLook : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField, Range(1f, 100f)] private float _sensitivity;
    [SerializeField, Range(0f, 45f)] private float _maxYOffset; 

    private TouchInputHandler _touchInputHandler;
    private ObjectPicker _objectPicker; 
    private float _xRotation;
    private Vector2 _lockAxis;

    private void Update()
    {
        RotatePlayerView();
        UpdateTargetPosition();
    }

    private void RotatePlayerView()
    {
        _lockAxis = _touchInputHandler.TouchDelta;
        float xMove = _lockAxis.x * _sensitivity * Time.deltaTime;
        float yMove = _lockAxis.y * _sensitivity * Time.deltaTime;

        _xRotation = Mathf.Clamp(_xRotation - yMove, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _objectPicker.transform.Rotate(Vector3.up * xMove);
    }

    private void UpdateTargetPosition()
    {
        if (_objectPicker != null)
        {
            float targetY = Mathf.Max(transform.position.y, transform.position.y + -_xRotation / 90f * _maxYOffset);
            _objectPicker.UpdateHoldPointPositionY(targetY);
        }
    }

    [Inject]
    private void Construct(ObjectPicker objectPicker, TouchInputHandler touchInputHandler)
    {
        _objectPicker = objectPicker;
        _touchInputHandler = touchInputHandler;
    }
}
