using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventGridManager : ScriptableObject 
{
    public Action ResetGrid;
    public Action ResetGridSlots;
    public Action ActualiseBoard;

    public void InvokeResetGrid() {  ResetGrid.Invoke(); }
    public void InvokeResetGridSlots() { ResetGridSlots.Invoke(); }
    public void InvokeActualiseBoard() { ActualiseBoard.Invoke(); }


}
