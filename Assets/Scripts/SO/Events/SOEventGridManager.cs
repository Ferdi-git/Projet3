using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventGridManager : ScriptableObject 
{
    public Action ResetGrid;

    public void InvokeResetGrid() {  ResetGrid.Invoke(); }


}
