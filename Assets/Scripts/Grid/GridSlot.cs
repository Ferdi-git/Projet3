using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public bool isFilled = false;
    public bool isAttacked;

    private GameObject pieceOnIt = null;

    public void SetPiece(GameObject piece)
    {
        pieceOnIt = piece;
        isFilled = piece != null;
    }

    public PieceAnimations GetPieceOnIt()
    {
        if (pieceOnIt == null) return null;
        return pieceOnIt.GetComponent<PieceAnimations>();
    }

    public void ClearSlot()
    {
        pieceOnIt = null;
        isFilled = false;
    }

    public void GetSelected() => isAttacked = true;
    public void GetDeselected() => isAttacked = false;

}