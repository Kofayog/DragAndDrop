using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyController : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _lookAction;

    [SerializeField] private float _movementSensitivity;
    [SerializeField] private float _lookSensitivity;

    private Vector2 _lastPosition;
    
    private void Move()
    {
        Vector2 moveVector = _moveAction.action.ReadValue<Vector2>();
        Vector3 movement = transform.forward * moveVector.y + transform.right * moveVector.x;
        
        transform.position += movement * (_movementSensitivity * Time.deltaTime);
    }
    private void Look()
    {
        Vector2 lookVector = _lookAction.action.ReadValue<Vector2>();

        var delta = lookVector - _lastPosition;

        if (Mouse.current.rightButton.isPressed)
        {
            var eulerAngles = transform.eulerAngles;
            var newYawAngle = eulerAngles.y + delta.x * _lookSensitivity * Time.deltaTime;
            var newRollAngle = eulerAngles.x - delta.y * _lookSensitivity * Time.deltaTime;
            
            transform.rotation = Quaternion.Euler(newRollAngle, newYawAngle, eulerAngles.z);

        }
        
        _lastPosition = lookVector;
    }

    void Update()
    {
        Move();
        Look();
    }
}
