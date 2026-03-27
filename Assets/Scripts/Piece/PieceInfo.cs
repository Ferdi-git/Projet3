using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.LightTransport;

public class PieceInfo : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip snapSound;
    [SerializeField] SOEventGridManager eventGrid;
    [SerializeField] SOEventGridManager sOEventGridManager;

    [SerializeField] private Transform[] surroundingPoints;
    public bool wasGridChecked = false;

    [SerializeField] private Transform[] posCases;
    [SerializeField] private LayerMask gridLayer;


    public Vector3 originalPos;
    public Quaternion originalRota;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SnapToGrid();

    }

    private void OnEnable()
    {
        sOEventGridManager.ResetPieceGridChecked += ResetPiece;
    }

    private void OnDisable()
    {
        sOEventGridManager.ResetPieceGridChecked -= ResetPiece;

    }

    public void Unfill()
    {
        foreach (var c in posCases)
        {
            foreach (var hit in Physics2D.OverlapPointAll(c.position, gridLayer))
            {
                GridSlot slot = hit.GetComponent<GridSlot>();
                if (slot != null) { slot.ClearSlot(); break; }
            }
        }
    }

    public void Refill()
    {
        foreach (var c in posCases)
        {
            foreach (var hit in Physics2D.OverlapPointAll(c.position, gridLayer))
            {
                GridSlot slot = hit.GetComponent<GridSlot>();
                if (slot != null) { slot.SetPiece(gameObject); break; }
            }
        }
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
                if (slot != null && !slot.isFilled) { slot.SetPiece(gameObject); break; }
            }
        }
        float randStartPitch = Random.Range(1.0f, 1.2f);
        audioSource.pitch = randStartPitch;
        audioSource.clip = snapSound;
        audioSource.Play();
        originalPos = transform.position;
        originalRota = transform.rotation;
        eventGrid.InvokePiecePlaced(this.gameObject);

    }


    public bool CheckIfCanBePlaced()
    {
        foreach (var c in posCases)
            if (!CheckIfSingleCaseCanBePlaced(c)) return false;

        return true;
    }

    public bool CheckIfSingleCaseCanBePlaced(Transform pos)
    {
        foreach (var hit in Physics2D.OverlapPointAll(pos.position, gridLayer))
        {
            GridSlot slot = hit.GetComponent<GridSlot>();
            if (slot != null && !slot.isFilled) return true;
        }

        return false;
    }

    public Transform[] GetSurroundingPoints()
    {
        return surroundingPoints;
    }
    public Transform[] GetSelfPoints()
    {
        return posCases;
    }

    private void ResetPiece()
    {
        wasGridChecked = false;
    }


}
