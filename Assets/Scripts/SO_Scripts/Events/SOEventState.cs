using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventState : ScriptableObject 
{
    public event Action StartShoping;
    public event Action EndOfShoping;
    public event Action StartCombat;
    public event Action EndOfCombat;

    public void InvokeStartShoping() { StartShoping?.Invoke(); }
    public void InvokeEndOfShoping() { EndOfShoping?.Invoke(); }
    public void InvokeStartCombat() { StartCombat?.Invoke(); }
    public void InvokeEndOfCombat() { EndOfCombat?.Invoke(); }
  
}
