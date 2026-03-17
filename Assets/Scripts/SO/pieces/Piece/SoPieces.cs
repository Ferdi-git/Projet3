using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPiece", menuName = "Pieces/Piece")]
public class SoPieces : ScriptableObject
{
    public PieceEffect pieceEffet;
    public List<int> ConditionValues;
    public List<int> EfectValues;


}
