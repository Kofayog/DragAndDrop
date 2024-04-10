using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputContext;
    
    private void Awake()
    {
        _inputContext.Enable();
    }

    private void OnDestroy()
    {
        _inputContext.Disable();
    }
}
