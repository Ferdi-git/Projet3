using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BoardPiece
{
    public SoPieces soPieces;
    public PiecePersonality piecePersonality;
    public List<BoardPiece> voisins = new List<BoardPiece>();
    public Context context;
    
    
}

