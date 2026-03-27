using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BoardPiece
{
    public int healthPoint;
    public int shield;

    public SoPieces soPieces;
    public PieceInfo pieceInfo;
    public PieceAnimations pieceAnimation;
    public Context context = new();


}

