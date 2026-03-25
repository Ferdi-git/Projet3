using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu]
public class PieceHealthManager : ScriptableObject
{
    public event Action PieceShieldBreak;
    public event Action PieceTakeDamage;
    public event Action PieceDie;

    public int hp;
    public int shield;



    public void GiveStats (int _hp, int _shield) {hp = _hp; shield = _shield;}
    public void TakeDamage (int damage)
    {
        if (shield <= 0)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                InvokeDead();
            }
            else
            {
                InvokeDamage();
            }
        }
        else
        {
            if (shield < damage)
            {
                hp -= damage - shield;
                shield = 0;
                InvokeShieldBreak();
                if (hp <= 0)
                {
                    hp = 0;
                    InvokeDead();
                }
                else
                {
                    InvokeDamage();
                }

            }
            else
            {
                shield -= damage;
                InvokeDamage();
            }
           
        }
    }

    public void InvokeDamage ()
    {
        PieceTakeDamage?.Invoke ();
    }

    public void InvokeShieldBreak ()
    {
        PieceShieldBreak?.Invoke ();
    }

    public void InvokeDead ()
    {
        PieceDie?.Invoke ();
    }
}
