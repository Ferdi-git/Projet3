using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Piece : MonoBehaviour
{
    [SerializeField] Transform[] posCases;
    [SerializeField] Transform[] posCotes;
    [SerializeField] private LayerMask gridLayer;


    private Transform originalPos;
    private Vector3 offset;
    private Camera mainCam;

    private void Start()
    {
        mainCam =Camera.main;
    }

    private void OnMouseDown()
    {
        print("Tried moving");
        originalPos = transform;
        offset = transform.position - GetMouseWorldPosition();
    }

    public void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + offset ;
    }

    private void OnMouseUp()
    {
        if (CheckIfCanBePlaced())
        {

        }
        else
        {
            transform.position = originalPos.position;
        }
        
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCam.WorldToScreenPoint(transform.position).z;
        return mainCam.ScreenToWorldPoint(mouseScreenPos);
    }

    private bool CheckIfCanBePlaced()
    {
        foreach(Transform caseP in posCases)
        {
            if (!CheckIfSingleCasefCanBePlaced(caseP))
            {
                return false;
            }
        }

        return true;

    }

    private bool CheckIfSingleCasefCanBePlaced(Transform pos)
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(pos.position, gridLayer);
        foreach (Collider2D hit in hits)
        {
            GridSlot slot = hit.GetComponent<GridSlot>();
            if (slot != null)
            {
                if (!slot.isFilled)
                    return true;
            }
        }
        return false;
    }

}
