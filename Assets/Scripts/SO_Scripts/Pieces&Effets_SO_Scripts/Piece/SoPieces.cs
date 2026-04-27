using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPiece", menuName = "Pieces/Piece")]
public class SoPieces : ScriptableObject
{
    public int healthPoint;
    public GameObject prefab;
    public Sprite image;
    public PieceEffect pieceEffet;
    public List<int> ConditionValues;
    public List<int> EffectValues;
    public List<PieceColor> colors = new List<PieceColor> { PieceColor.Neutral};
    public string description;
    public bool isRepetition = false;
    public int numberManaGeneratingPerSquare;

    public enum PieceColor
    {
        Neutral,
        Red,
        Yellow,
        Blue,
    }

}
