using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu]
public class PieceHealthManager : ScriptableObject
{
    

    public int hp;
    public int shield;
    public BoardPiece piece;

    public SOEventPieceHealth pieceHealthEvent;


    public void GiveStats (int _hp, int _shield , BoardPiece boardPiece) {hp = _hp; shield = _shield;piece = boardPiece; }
    public void TakeDamage (int damage)
    {
        if (shield <= 0)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                Dead();
            }
            else
            {
                TakeDamage();
            }
        }
        else
        {
            if (shield < damage)
            {
                hp -= damage - shield;
                shield = 0;
                ShieldBreak();
                if (hp <= 0)
                {
                    hp = 0;
                    Dead();
                }
                else
                {
                    TakeDamage();
                }

            }
            else
            {
                shield -= damage;
                TakeDamage();
            }
           
        }
    }

    public void TakeDamage ()
    {
        pieceHealthEvent.InvokeDamage(piece);
    }

    public void ShieldBreak ()
    {
        pieceHealthEvent.InvokeShieldBreak(piece);
    }

    public void Dead ()
    {
        pieceHealthEvent.InvokeDead(piece);
    }
}
