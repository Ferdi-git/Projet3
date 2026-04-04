using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventEnnemy : ScriptableObject
{
    public Action<int> EnnemiGainPV;
    public Action<int> EnnemiLoosePV;


    public Action<int> EnnemiGainShield;
    public Action<int> EnnemiLooseShield;


    public Action NewEnnemi;

    public void InvokeEnnemiGainPV(int amount) { EnnemiGainPV?.Invoke(amount); }
    public void InvokeEnnemiLoosePV(int amount) { EnnemiLoosePV?.Invoke(amount); }
    public void InvokeEnnemiGainShield(int amount) { EnnemiGainShield?.Invoke(amount); }
    public void InvokeEnnemiLooseShield(int amount) { EnnemiLooseShield?.Invoke(amount); }

    public void InvokeNewEnnemi () { NewEnnemi?.Invoke(); }

}
