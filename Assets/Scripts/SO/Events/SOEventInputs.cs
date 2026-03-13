using System;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputsEvent", menuName = "Events/Inputs")]
public class SOEventInputs : ScriptableObject
{
    public Action<InputAction.CallbackContext> OnClick;

    public void InvokeClick(InputAction.CallbackContext context) => OnClick?.Invoke(context);
}
