using DG.Tweening;
using UnityEngine;

public class PieceMouvement : MonoBehaviour, IMouseDraggable, IMouseHoverable, IMouseClickable
{
    [SerializeField] private Transform[] posCases;
    [SerializeField] private LayerMask gridLayer;

    [SerializeField] SOEventGridManager eventGrid;

    AudioSource audioSource;
    [SerializeField] AudioClip snapSound;

    private Vector3 originalPos;
    private Quaternion originalRota;

    public bool isDraging = false;
    public bool isRotating = false;
    public bool isRotatingInputBuffer = false;

    public bool canBeMoved = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SnapToGrid();
    }

    public void OnDragStart(Vector2 worldPos)
    {
        if (!canBeMoved) return;
        isDraging = true;
        originalPos = transform.position;
        originalRota = transform.rotation;
        transform.DOScale(1.1f, 0.1f);
        Unfill();
    }

    public void OnDragMove(Vector2 worldPos)
    {
        if(!isDraging) return;
        Vector2 targetPos = worldPos;
        transform.position = new Vector3(targetPos.x, targetPos.y + 0.1f, -0.1f);
    }

    public void OnDragEnd(Vector2 worldPos)
    {
        if (!isDraging) return;
        isDraging = false;
        isRotatingInputBuffer = false;
        isRotating = false;

        transform.DOKill();
        transform.DOScale(1f, 0.1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (CheckIfCanBePlaced())
        {
            float snappedZ = Mathf.Round(transform.eulerAngles.z / 90f) * 90f;
            transform.rotation = Quaternion.Euler(0, 0, snappedZ);
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

    public void SnapToGrid()
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
        float randStartPitch = Random.Range(1.0f, 1.2f);
        audioSource.pitch = randStartPitch;
        audioSource.clip = snapSound;
        audioSource.Play();
        eventGrid.InvokePiecePlaced(this.gameObject);

    }


    public void OnClick()
    {
        // nothing
    }

    public void OnRightClick()
    {
        if (!isDraging) return;

        if (isRotating)
        {
            isRotatingInputBuffer = true;
            return;
        }

        isRotating = true;

        float currentZ = transform.eulerAngles.z;
        float targetZ = currentZ - 90f;

        transform.DORotate(new Vector3(0, 0, targetZ), 0.2f, RotateMode.FastBeyond360).OnComplete(() =>
        {
            isRotating = false;
            if (isRotatingInputBuffer)
            {
                isRotatingInputBuffer = false;
                OnRightClick();
            }
        });
    }


    public void SetCanMove(bool can)
    {
        canBeMoved = can;
    }
}