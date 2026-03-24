using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventGridManager : ScriptableObject 
{
    public event Action ResetPieceGridChecked;
    public event Action ResetGridSlots;
    public event Action ActualiseBoard;
    public event Action<GameObject> PiecePlaced;
    public event Action OnePieceIsPlaced;
    public event Action SaveInventory;
    public event Action ResetInventory;
    public event Action<GameObject> AddBoardPiece;

    public void InvokeResetPieceGridCheckedd() { ResetPieceGridChecked?.Invoke(); }
    public void InvokeResetGridSlots() { ResetGridSlots?.Invoke(); }
    public void InvokeActualiseBoard() { ActualiseBoard?.Invoke(); }

    public void InvokeSaveInventory() {  SaveInventory?.Invoke(); }

    public void InvokeResetInventory() {  ResetInventory?.Invoke(); }

    public void InvokePiecePlaced(GameObject piece)
    {
        PiecePlaced?.Invoke(piece);
        OnePieceIsPlaced?.Invoke();
    }

    public void InvokeAddBoardPiece(GameObject go) { AddBoardPiece.Invoke(go); }
}
