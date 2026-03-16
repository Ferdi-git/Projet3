using System;
using UnityEngine;
[Serializable]
public class BoardPiece
{
    public SoPieces soPieces;
    public PiecePersonality piecePersonality;
    [HideInInspector] public BoardPiece[] voisins;
    public Context context;
    
    
}

