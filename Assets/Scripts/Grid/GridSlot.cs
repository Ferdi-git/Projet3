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

    public PieceInfo GetPieceOnIt()
    {
        if (pieceOnIt == null) return null;
        return pieceOnIt.GetComponent<PieceInfo>();
    }
    public SinglePieceSquare GetSinglePieceOnIt()
    {
        if (pieceOnIt == null) return null;
        return pieceOnIt.GetComponent<SinglePieceSquare>();
    }

    public void ClearSlot()
    {
        pieceOnIt = null;
        isFilled = false;
    }

    public void GetSelected() => isAttacked = true;
    public void GetDeselected() => isAttacked = false;

}