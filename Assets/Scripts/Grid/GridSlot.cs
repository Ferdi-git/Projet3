using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public bool isFilled = false;

    public PiecePersonality GetPieceOnIt()
    {
        foreach (var hit in Physics2D.OverlapPointAll(transform.position))
            if (hit.gameObject.GetComponent<PiecePersonality>() != null)
                return hit.gameObject.GetComponent<PiecePersonality>();
        return null;
    }
}
