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




    // appel de fonction de l'ennemi Manager

    public event Action GenerateEnnemi;

    public event Action EnnemiShowAttack;

    //public event Action<int> EnnemiGetAttackZoneNbr;


    public void InvokeGenerateEnnemi ()
    {
        GenerateEnnemi?.Invoke();
    }
    public void InvokeEnnemiShowAttack()
    {
        EnnemiShowAttack?.Invoke();
    }
    

}
