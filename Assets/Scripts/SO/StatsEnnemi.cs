using System;
using UnityEngine;

[CreateAssetMenu]
public class StatsEnnemi : ScriptableObject
{
    public int pv;
    public int shield;

    public Action<int> EnnemiGainPV;
    public Action<int> EnnemiLoosePV;


    public Action<int> EnnemiGainShield;
    public Action<int> EnnemiLooseShield;

    public void InvokeEnnemiGainPV(int amount)
    {
        pv += amount;
        EnnemiGainPV?.Invoke(amount);
    }
    public void InvokeEnnemiLoosePV(int amount)
    {
        pv -= amount;
        EnnemiLoosePV?.Invoke(amount);
    }

    public void InvokeEnnemiGainShield(int amount) { shield += amount; EnnemiGainShield?.Invoke(amount); }
    public void InvokeEnnemiLooseShield(int amount) 
    { 
        
        if (amount <= shield)
        {
            shield -= amount;
            EnnemiLooseShield?.Invoke(amount);
        }
        else
        {
            EnnemiLooseShield?.Invoke(shield);
            shield = 0;
            InvokeEnnemiLoosePV(amount - shield);
        }
    }



    public int GetPV()
    {
        return pv;
    }

    public int GetShield()
    {
        return shield;
    }
}
