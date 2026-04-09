using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventPieceHealth : ScriptableObject
{
    public event Action<BoardPiece> PieceShieldBreak;
    public event Action<BoardPiece> PieceTakeDamage;
    public event Action<BoardPiece> PieceDie;

    public void InvokeDamage(BoardPiece piece)
    {
        PieceTakeDamage?.Invoke(piece);
    }

    public void InvokeShieldBreak(BoardPiece piece)
    {
        PieceShieldBreak?.Invoke(piece);
    }

    public void InvokeDead(BoardPiece piece)
    {
        PieceDie?.Invoke(piece);
    }
}
