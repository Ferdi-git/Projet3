using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public bool isFilled = false;
    public bool isAttacked;



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

    public void GetDeselected()
    {
        isAttacked = false;
    }
}
