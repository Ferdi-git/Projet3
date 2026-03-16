using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoBoard : ScriptableObject
{
    public List<BoardPiece> boardPieces = new List<BoardPiece>();

    public List<List<BoardPiece>> voisins = new List<List<BoardPiece>>();
}
