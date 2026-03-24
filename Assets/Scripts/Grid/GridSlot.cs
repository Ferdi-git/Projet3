using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public bool isFilled = false;
    public SpriteRenderer spriteR;
    public Color baseColor;
    public bool isAttacked;

    private void Start()
    {
        baseColor  = spriteR.color;
    }

    public PieceAnimations GetPieceOnIt()
    {
        foreach (var hit in Physics2D.OverlapPointAll(transform.position))
            if (hit.gameObject.GetComponent<PieceAnimations>() != null)
                return hit.gameObject.GetComponent<PieceAnimations>();
        return null;
    }

    public void GetSelected()
    {
        isAttacked = true;
    }

    public void GetReseted()
    {
        isAttacked = false;
    }
}
