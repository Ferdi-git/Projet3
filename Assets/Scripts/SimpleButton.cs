using UnityEngine;
using UnityEngine.Events;

public class SimpleButton : MonoBehaviour, IMouseClickable, IMouseHoverable
{
    [SerializeField] private UnityEvent onClick;


    public void OnClick()
    {
        onClick.Invoke();
    }

    public void OnHoverEnter()
    {
        //
    }

    public void OnHoverExit()
    {
        //
    }
}
