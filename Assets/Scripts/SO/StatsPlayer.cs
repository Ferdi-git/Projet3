using System;
using UnityEngine;

[CreateAssetMenu]
public class StatsPlayer : ScriptableObject
{
    public int pv;
    public int shield;

    public Action<int> GainPV ;
    public Action<int> LoosePV;


    public Action<int> GainShield;
    public Action<int> LooseShield;


    public void InvokeGainPV(int amount ) 
    {
        pv += amount; 
        GainPV?.Invoke(amount);
    }
    public void InvokeLoosePV(int amount) 
    { 
        pv -= amount;
        LoosePV?.Invoke(amount);
    }

    public void InvokeGainShield(int amount) { shield += amount;  GainShield?.Invoke(amount); }
    public void InvokeLooseShield(int amount) { shield -= amount; LooseShield?.Invoke(amount); }



    public int GetPV ()
    {
        return pv;
    }

    public int GetShield ()
    {
        return shield;  
    }
}
