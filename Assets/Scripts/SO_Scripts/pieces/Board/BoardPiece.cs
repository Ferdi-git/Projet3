using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BoardPiece
{
    public float healthPoint;
    public float shield;

    public SoPieces soPieces;
    public PieceAnimations piecePersonality;
    public Context context = new();


}

