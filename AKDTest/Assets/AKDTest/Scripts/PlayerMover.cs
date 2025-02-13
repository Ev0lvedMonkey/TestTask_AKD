using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController _controller;

    [Header("Properties")]
    [SerializeField, Range(0.5f, 10f)] private float _moveSpeed;

    private FixedJoystick _joystick;

    private void OnValidate()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = transform.right * _joystick.Horizontal + transform.forward * _joystick.Vertical;
        _controller.Move(moveDirection * _moveSpeed * Time.deltaTime);
    }

    [Inject]
    private void Construct(FixedJoystick joystick)
    {
        _joystick = joystick;
    }
}