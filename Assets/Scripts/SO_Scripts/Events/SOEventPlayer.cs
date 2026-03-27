using System;
using UnityEngine;

[CreateAssetMenu]
public class SOEventPlayer : ScriptableObject
{
    public Action<int> GainPV;
    public Action<int> LoosePV;


    public Action<int> GainShield;
    public Action<int> LooseShield;

    public void InvokePlayerGainPV(int amount) { GainPV?.Invoke(amount); }
    public void InvokePlayerLoosePV(int amount) { LoosePV?.Invoke(amount); }
    public void InvokePlayerGainShield(int amount) { GainShield?.Invoke(amount); }
    public void InvokePlayerLooseShield(int amount) { LooseShield?.Invoke(amount); }
}
