using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    private Camera mainCam;
    private InputSystem_Actions input;
    private IMouseHoverable currentHovered;
    private IMouseDraggable currentDragged;
    private bool isDragging;

    private void Awake()
    {
        mainCam = Camera.main;
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Player.Click.started += OnClickStarted;
        input.Player.Click.canceled += OnClickCanceled;
        input.Player.Click.Enable();
        input.Player.RightClick.started += OnRightClickStarted;
        input.Player.RightClick.canceled += OnRightClickCanceled;
        input.Player.RightClick.Enable();
    }

    private void OnDisable()
    {
        input.Player.Click.started -= OnClickStarted;
        input.Player.Click.canceled -= OnClickCanceled;
        input.Player.Click.Disable();
        input.Player.RightClick.started -= OnRightClickStarted;
        input.Player.RightClick.canceled -= OnRightClickCanceled;
        input.Player.RightClick.Disable();
    }

    private void Update()
    {
        Vector2 mousePos = GetMouseWorldPos();

        //Hover
        if (!isDragging)
        {
            IMouseHoverable hovered = null;
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
            foreach (var hit in hits)
            {
                hovered = hit.GetComponent<IMouseHoverable>();
                if (hovered != null) break;
            }

            if (hovered != currentHovered)
            {
                currentHovered?.OnHoverExit();
                currentHovered = hovered;
                currentHovered?.OnHoverEnter();
            }
        }

        // Drag
        if (isDragging && currentDragged != null)
            currentDragged.OnDragMove(mousePos);
    }

    private void OnClickStarted(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = GetMouseWorldPos();

        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
        foreach (var hit in hits)
        {
            currentDragged = hit.GetComponent<IMouseDraggable>();
            if (currentDragged != null)
            {
                isDragging = true;
                currentDragged.OnDragStart(mousePos);

                currentHovered?.OnHoverExit();
                currentHovered = null;

                break;
            }
        }
    }

    private void OnRightClickStarted(InputAction.CallbackContext ctx)
    {
        //Vector2 mousePos = GetMouseWorldPos();

        //Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
        //foreach (var hit in hits)
        //{
        //    currentDragged = hit.GetComponent<IMouseDraggable>();
        //    if (currentDragged != null)
        //    {
        //        isDragging = true;
        //        currentDragged.OnDragStart(mousePos);

        //        currentHovered?.OnHoverExit();
        //        currentHovered = null;

        //        break;
        //    }
        //}
    }

    private void OnRightClickCanceled(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = GetMouseWorldPos();

        if (isDragging && currentDragged != null)
        {
            // Click (only reached if nothing was dragged)
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
            foreach (var hit in hits)
            {
                IMouseClickable clicked = hit.GetComponent<IMouseClickable>();
                if (clicked != null) { clicked.OnRightClick(); break; }
            }

        }
    }
    private void OnClickCanceled(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = GetMouseWorldPos();

        if (isDragging && currentDragged != null)
        {
            currentDragged.OnDragEnd(mousePos);
            currentDragged = null;
            isDragging = false;
            return;
        }

        // Click (only reached if nothing was dragged)
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
        foreach (var hit in hits)
        {
            IMouseClickable clicked = hit.GetComponent<IMouseClickable>();
            if (clicked != null) { clicked.OnClick(); break; }
        }
    }

    private Vector2 GetMouseWorldPos()
    {
        Vector3 screen = Input.mousePosition;
        screen.z = Mathf.Abs(mainCam.transform.position.z);
        return mainCam.ScreenToWorldPoint(screen);
    }
}