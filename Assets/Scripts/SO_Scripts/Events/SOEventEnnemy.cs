using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventEnnemy : ScriptableObject
{
    public event Action<int> EnnemiGainPV;
    public event Action<int> EnnemiLoosePV;


    public event Action<int> EnnemiGainShield;
    public event Action<int> EnnemiLooseShield;


    public event Action NewEnnemi;

    public void InvokeEnnemiGainPV(int amount) { EnnemiGainPV?.Invoke(amount); }
    public void InvokeEnnemiLoosePV(int amount) { EnnemiLoosePV?.Invoke(amount); }
    public void InvokeEnnemiGainShield(int amount) { EnnemiGainShield?.Invoke(amount); }
    public void InvokeEnnemiLooseShield(int amount) { EnnemiLooseShield?.Invoke(amount); }

    public void InvokeNewEnnemi () { NewEnnemi?.Invoke(); }

}
