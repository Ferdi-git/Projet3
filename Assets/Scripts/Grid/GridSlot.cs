using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public bool isFilled = false;
    public SpriteRenderer spriteR;
    public Color baseColor;

    private void Start()
    {
        baseColor  = spriteR.color;
    }

    public PiecePersonality GetPieceOnIt()
    {
        foreach (var hit in Physics2D.OverlapPointAll(transform.position))
            if (hit.gameObject.GetComponent<PiecePersonality>() != null)
                return hit.gameObject.GetComponent<PiecePersonality>();
        return null;
    }

    public void GetSelected()
    {
        spriteR.color = Color.red;
    }

    public void GetReseted()
    {
        spriteR.color = baseColor;
    }
}
