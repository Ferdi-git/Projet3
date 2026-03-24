using UnityEngine;
using UnityEngine.Events;

public class SimpleButton : MonoBehaviour, IMouseClickable, IMouseHoverable
{
    [SerializeField] private UnityEvent onClick;


    public void OnClick()
    {
        print("ButtonClicked");
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

    public void OnRightClick()
    {
        //
    }
}
