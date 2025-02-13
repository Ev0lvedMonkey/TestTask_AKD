using UnityEngine.EventSystems;
using UnityEngine;
using Zenject;

public class TouchInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LayerMask _pickUpLayer;

    public Vector2 TouchDelta { get; private set; }

    private ObjectPicker _objectPicker;
    private Vector2 _previousPointerPosition;
    private int _pointerId;
    private bool _isPressed;
    private PickableObject _currentPickableObject;

    private void Update()
    {
        ProcessTouchInput();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        _pointerId = eventData.pointerId;
        _previousPointerPosition = eventData.position;

        TryPickOrDropObject(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }

    private void ProcessTouchInput()
    {
        if (_isPressed)
            UpdateTouchDelta();
        else
            TouchDelta = Vector2.zero;
    }

    private void UpdateTouchDelta()
    {
        if (_pointerId >= 0 && _pointerId < Input.touches.Length)
        {
            TouchDelta = Input.touches[_pointerId].position - _previousPointerPosition;
            _previousPointerPosition = Input.touches[_pointerId].position;
        }
        else
        {
            TouchDelta = (Vector2)Input.mousePosition - _previousPointerPosition;
            _previousPointerPosition = Input.mousePosition;
        }
    }

    private void TryPickOrDropObject(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _pickUpLayer))
        {
            if (hit.collider.TryGetComponent(out PickableObject pickableObject))
                HandleObjectPickOrDrop(pickableObject);
        }
    }

    private void HandleObjectPickOrDrop(PickableObject pickableObject)
    {
        if (_currentPickableObject == null)
        {
            _currentPickableObject = pickableObject;
            _objectPicker.PickUp(pickableObject);
            Debug.Log($"Picked up: {pickableObject.gameObject.name}");
        }
        else if (_currentPickableObject == pickableObject)
        {
            _objectPicker.Drop();
            _currentPickableObject = null;
            Debug.Log($"Dropped: {pickableObject.gameObject.name}");
        }
    }

    [Inject]
    private void Construct(ObjectPicker objectPicker)
    {
        _objectPicker = objectPicker;
    }
}
