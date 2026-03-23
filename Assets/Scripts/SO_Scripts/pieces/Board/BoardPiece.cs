using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BoardPiece
{
    public float healthPoint;
    public float shield;

    public SoPieces soPieces;
    public PiecePersonality piecePersonality;
    public Context context = new();


}

