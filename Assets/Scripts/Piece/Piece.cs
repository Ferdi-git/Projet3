using DG.Tweening;
using UnityEngine;

public class Piece : MonoBehaviour, IMouseDraggable, IMouseHoverable
{
    [SerializeField] private Transform[] posCases;
    [SerializeField] private LayerMask gridLayer;

    private Vector3 originalPos;
    private Vector2 grabOffset;

    public void OnDragStart(Vector2 worldPos)
    {
        originalPos = transform.position;
        grabOffset = (Vector2)transform.position - worldPos;
        transform.DOScale(1.1f, 0.1f);
        Unfill();
    }

    public void OnDragMove(Vector2 worldPos)
    {
        Vector2 targetPos = worldPos + grabOffset;
        transform.position = new Vector3(targetPos.x, targetPos.y +0.1f, -0.1f);
    }

    public void OnDragEnd(Vector2 worldPos)
    {
        transform.DOScale(1f, 0.1f);

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
        }
    }

    public void OnHoverEnter() => transform.DOScale(1.05f, 0.1f);
    public void OnHoverExit() => transform.DOScale(1f, 0.1f);

    private void Unfill()
    {
        foreach (var c in posCases)
            foreach (var hit in Physics2D.OverlapPointAll(c.position, gridLayer))
            {
                GridSlot slot = hit.GetComponent<GridSlot>();
                if (slot != null) { slot.isFilled = false; break; }
            }
    }

    private void Refill()
    {
        foreach (var c in posCases)
            foreach (var hit in Physics2D.OverlapPointAll(c.position, gridLayer))
            {
                GridSlot slot = hit.GetComponent<GridSlot>();
                if (slot != null) { slot.isFilled = true; break; }
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
            foreach (var hit in Physics2D.OverlapPointAll(c.position, gridLayer))
            {
                GridSlot slot = hit.GetComponent<GridSlot>();
                if (slot != null && !slot.isFilled) { slot.isFilled = true; break; }
            }
    }
}