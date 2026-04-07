using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StatsEnnemi : ScriptableObject
{
    public int pv;
    public int shield;
    public int pvMax;
    public string ennemiName;
    public Sprite sprite;
    public List<EnnemiAttack> ennemiAttacks;
    public SOEventEnnemy ennemyEvent;


    [Header("Average hp an ennemy has for the first ennemi ")]
    public int AverageValue;

    

    public void EnnemiGainPV(int amount)
    {
        pv += amount;
        ennemyEvent.InvokeEnnemiGainPV(amount);
    }
    public void EnnemiLoosePV(int amount)
    {
        if (pv > amount)
        {
            pv -= amount;
            ennemyEvent.InvokeEnnemiLoosePV(amount);
        }
        else
        {
            pv = 0;
            ennemyEvent.InvokeEnnemiLoosePV(pv);
        }

        
    }

    public void EnnemiGainShield(int amount) { shield += amount; ennemyEvent.InvokeEnnemiGainShield(amount); }
    public void EnnemiTakeDamager(int amount) 
    { 
        
        if (amount <= shield)
        {
            shield -= amount;
            ennemyEvent.InvokeEnnemiLooseShield(amount);
        }
        else
        {
            ennemyEvent.InvokeEnnemiLooseShield(shield);
            shield = 0;
            EnnemiLoosePV(amount - shield);
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
