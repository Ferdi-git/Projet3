using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu]
public class PieceHealthManager : ScriptableObject
{
    public event Action<BoardPiece> PieceShieldBreak;
    public event Action<BoardPiece> PieceTakeDamage;
    public event Action<BoardPiece> PieceDie;

    public int hp;
    public int shield;
    public BoardPiece piece;



    public void GiveStats (int _hp, int _shield , BoardPiece boardPiece) {hp = _hp; shield = _shield;piece = boardPiece; }
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
        PieceTakeDamage?.Invoke (piece);
    }

    public void InvokeShieldBreak ()
    {
        PieceShieldBreak?.Invoke (piece);
    }

    public void InvokeDead ()
    {
        PieceDie?.Invoke (piece);
    }
}
