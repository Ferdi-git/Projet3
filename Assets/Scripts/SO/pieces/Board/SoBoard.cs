using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoBoard : ScriptableObject
{
    public List<BoardPiece> boardPieces;

    public List<List<BoardPiece>> Voisins;
}
