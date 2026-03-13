using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private SOEventInputs eventInputs;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void OnEnable()
    {

        eventInputs.OnClick += OnClick;
    }

    private void OnDisable()
    {
        
    }

    void Update()
    {
        transform.position = GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCam.WorldToScreenPoint(transform.position).z;
        return mainCam.ScreenToWorldPoint(mouseScreenPos);
    }


    private void OnClick(InputAction.CallbackContext context)
    {

    }
}
