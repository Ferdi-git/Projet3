using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventState : ScriptableObject 
{
    public event Action StartShoping;
    public event Action EndOfShoping;

    public void InvokeStartShoping() { StartShoping?.Invoke(); }
    public void InvokeEndOfShoping() { EndOfShoping?.Invoke(); }




    public event Action StartCombat;
    public event Action EndOfCombat;
    public void InvokeStartCombat() { StartCombat?.Invoke(); }
    public void InvokeEndOfCombat() { EndOfCombat?.Invoke(); }



    //meurt 

    //Victoire

    public event Action WinEvent;
    public event Action LooseEvent;

    public void InvokeWinEvent() { WinEvent?.Invoke(); }
    public void InvokeLooseEvent() { LooseEvent?.Invoke(); }

}
