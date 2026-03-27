using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class StatsPlayer : ScriptableObject
{
    public int pv;
    public int shield;

    public int pvMax;

    public SOEventPlayer player;

    
    public void InvokeGainPV(int amount ) 
    {
        pv += amount;
        player.InvokePlayerGainPV( amount );
    }
    public void InvokeLoosePV(int amount) 
    { 
        pv -= amount;
        player.InvokePlayerLoosePV( amount );
    }

    public void InvokeGainShield(int amount) { shield += amount; player.InvokePlayerGainShield(amount); }
    public void InvokeTakeDamage(int amount) 
    { 
        if (amount <= shield)
        {
            shield -= amount;
            player.InvokePlayerLooseShield( amount );
        }
        else
        {
            player.InvokePlayerLooseShield(amount);
            shield = 0;
            InvokeLoosePV(amount - shield);
        }
        

    }



    public int GetPV ()
    {
        return pv;
    }

    public int GetShield ()
    {
        return shield;  
    }
}
