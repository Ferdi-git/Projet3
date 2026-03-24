using UnityEngine;
using UnityEngine.Events;
using UnityEngine.LightTransport;

public class SimpleButton : MonoBehaviour, IMouseClickable, IMouseHoverable
{
    [SerializeField] private UnityEvent onClick;
    public SOEventGridManager eventGrid;

    public bool canBeClicked;

    private void OnEnable()
    {
        eventGrid.SetAllPieceCanMove += SetCanBeClicked;
    }

    private void OnDisable()
    {
        eventGrid.SetAllPieceCanMove -= SetCanBeClicked;

    }

    public void OnClick()
    {
        if(!canBeClicked) {return; }
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

    public void SetCanBeClicked(bool can)
    {
        canBeClicked = can;
    }
}
