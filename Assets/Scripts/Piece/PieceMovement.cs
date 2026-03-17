using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PieceMouvement : MonoBehaviour, IMouseDraggable, IMouseHoverable , IMouseClickable
{
    [SerializeField] private Transform[] posCases;
    [SerializeField] private LayerMask gridLayer;

    private Vector3 originalPos;
    private Quaternion originalRota;

    public bool isDraging = false;

    public bool isRotating = false;
    public bool isRotatingInputBuffer = false;


    private void Start()
    {
        SnapToGrid();
    }

    public void OnDragStart(Vector2 worldPos)
    {
        isDraging = true;
        originalPos = transform.position;
        originalRota = transform.rotation;
        transform.DOScale(1.1f, 0.1f);
        Unfill();
    }

    public void OnDragMove(Vector2 worldPos)
    {
        Vector2 targetPos = worldPos;
        transform.position = new Vector3(targetPos.x, targetPos.y +0.1f, -0.1f);
    }

    public void OnDragEnd(Vector2 worldPos)
    {
        isDraging = false;
        transform.DOScale(1f, 0.1f);

        transform.position = new Vector3(transform.position.x, transform.position.y , 0);

        if (CheckIfCanBePlaced())
        {
            SnapToGrid();
        }
        else
        {
            transform.DOMove(originalPos, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                Refill();
            });

            transform.DORotateQuaternion(originalRota, 0.2f).SetEase(Ease.OutBack);
        }
    }

    public void OnHoverEnter()
    {
        transform.DOScale(1.05f, 0.1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);

    }

    public void OnHoverExit() 
    { 
        transform.DOScale(1f, 0.1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

    }

    private void Unfill()
    {
        foreach (var c in posCases)
        {
            foreach (var hit in Physics2D.OverlapPointAll(c.position, gridLayer))
            {
                GridSlot slot = hit.GetComponent<GridSlot>();
                if (slot != null) { slot.isFilled = false; break; }
            }
        }
    }

    private void Refill()
    {
        foreach (var c in posCases)
        {
            foreach (var hit in Physics2D.OverlapPointAll(c.position, gridLayer))
            {
                GridSlot slot = hit.GetComponent<GridSlot>();
                if (slot != null) { slot.isFilled = true; break; }
            }
        }
    }

    private bool CheckIfCanBePlaced()
    {
        foreach (var c in posCases)
            if (!CheckIfSingleCaseCanBePlaced(c)) return false;

        return true;
    }

    private bool CheckIfSingleCaseCanBePlaced(Transform pos)
    {
        foreach (var hit in Physics2D.OverlapPointAll(pos.position, gridLayer))
        {
            GridSlot slot = hit.GetComponent<GridSlot>();
            if (slot != null && !slot.isFilled) return true;
        }

        return false;
    }

    private void SnapToGrid()
    {
        GridSlot targetSlot = null;
        Vector3 targetSlotPos = Vector3.zero;


        Vector2 samplePos = (Vector2)posCases[0].position;

        foreach (var hit in Physics2D.OverlapPointAll(samplePos, gridLayer))
        {
            GridSlot slot = hit.GetComponent<GridSlot>();
            if (slot != null && !slot.isFilled)
            {
                targetSlot = slot;
                targetSlotPos = slot.transform.position;
                break;
            }
        }

        if (targetSlot == null) return;


        transform.position = targetSlotPos;

        foreach (var c in posCases)
        {
            foreach (var hit in Physics2D.OverlapPointAll(c.position, gridLayer))
            {
                GridSlot slot = hit.GetComponent<GridSlot>();
                if (slot != null && !slot.isFilled) { slot.isFilled = true; break; }
            }
        }


    }

    public void OnClick()
    {
        //nothing
    }

    public void OnRightClick()
    {
        print("Rotate");
        if (isRotating || !isDraging)
        {
            isRotatingInputBuffer = true;
            return;
        }

        isRotating = true;
        //rotateGoal -= 90;
        transform.DORotate(new Vector3(0,0, (int)transform.rotation.z / 90 * 90 -90), 0.2f, RotateMode.LocalAxisAdd).OnComplete(()=>
        { 
            isRotating = false;
            if (isRotatingInputBuffer)
            {
                isRotatingInputBuffer = false;
                OnRightClick();
            }
           
        });

        //transform.DORotate(transform.right * 90, 0.5f, RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine);
        //transform.rotation *= Quaternion.Euler(0, 0, -90); ;


    }
}