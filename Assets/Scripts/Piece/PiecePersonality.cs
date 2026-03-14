
using UnityEngine;

public class PiecePersonality : MonoBehaviour
{

    [SerializeField] private SoPieces soPieces;
    public PiecePersonality[] surroundingPieces;
    [SerializeField] private Transform[] posCases;
    [SerializeField] SOEventGridManager sOEventGridManager;

    public bool wasUsed = false;

    private void OnEnable()
    {
        sOEventGridManager.ResetGrid += ResetPiece;
    }

    private void OnDisable()
    {
        sOEventGridManager.ResetGrid -= ResetPiece;

    }

    public bool hasBeenUsed = false;

    public string TEMPORARESTRING;

    public string GetContext()
    {
        wasUsed = true;
        return TEMPORARESTRING;
    }

    private void ResetPiece()
    {
        wasUsed = false;
    }

}


public class PieceContext
{
    public PiecePersonality personality;
    public ActionEffet effets;

}
