using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions playerInput;
    public SOEventInputs soEventInputs;

    private void Awake()
    {
        playerInput = new InputSystem_Actions();
        
    }

    private void OnEnable()
    {
        playerInput.Player.Click.performed += soEventInputs.InvokeClick;
        playerInput.Player.Click.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Click.performed -= soEventInputs.InvokeClick;
        playerInput.Player.Click.Enable();
    }

}
