using UnityEngine;
using UnityEngine.InputSystem;

public interface IMouseHoverable
{
    void OnHoverEnter();
    void OnHoverExit();
}

public interface IMouseClickable
{
    void OnClick();
}

public interface IMouseDraggable
{
    void OnDragStart(Vector2 worldPos);
    void OnDragMove(Vector2 worldPos);
    void OnDragEnd(Vector2 worldPos);
}