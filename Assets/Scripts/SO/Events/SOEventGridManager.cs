using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventGridManager : ScriptableObject 
{
    public Action ResetGrid;
    public Action ResetGridSlots;
    public Action ActualiseBoard;
    public Action<GameObject> PiecePlaced;
    public Action SaveInventory;
    public Action ResetInventory;

    public void InvokeResetGrid() {  ResetGrid?.Invoke(); }
    public void InvokeResetGridSlots() { ResetGridSlots?.Invoke(); }
    public void InvokeActualiseBoard() { ActualiseBoard?.Invoke(); }

    public void InvokeSaveInventory() {  SaveInventory?.Invoke(); }

    public void InvokeResetInventory() {  ResetInventory?.Invoke(); }

    public void InvokePiecePlaced(GameObject piece)
    {
        PiecePlaced?.Invoke(piece);
    }
}
