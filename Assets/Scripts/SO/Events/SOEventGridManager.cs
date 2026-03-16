using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventGridManager : ScriptableObject 
{
    public Action ResetGrid;
    public Action ActualiseBoard;

    public void InvokeResetGrid() {  ResetGrid.Invoke(); }
    public void InvokeActualiseBoard() {  ResetGrid.Invoke(); }


}
