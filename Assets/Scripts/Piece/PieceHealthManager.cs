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
                LooseHp();
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
                    LooseHp();
                }

            }
            else
            {
                shield -= damage;
                LooseHp();
            }
           
        }
    }

    public void LooseHp ()
    {
        ResetStatsPiece();
        pieceHealthEvent.InvokeDamage(piece);
    }

    public void ShieldBreak ()
    {
        ResetStatsPiece();
        pieceHealthEvent.InvokeShieldBreak(piece);
    }

    public void Dead ()
    {
        ResetStatsPiece();
        pieceHealthEvent.InvokeDead(piece);
    }

    private void ResetStatsPiece ()
    {
        piece.healthPoint = hp;
        piece.shield = shield;
    }
}
