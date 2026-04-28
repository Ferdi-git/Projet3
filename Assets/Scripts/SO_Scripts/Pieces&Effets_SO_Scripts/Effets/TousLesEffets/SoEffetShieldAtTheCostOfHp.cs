using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Audio.ProcessorInstance;

[CreateAssetMenu(fileName = "ShieldAtTheCostOfHpEffet", menuName = "Effet/ShieldAtTheCostOfHp")]
public class SoEffetShieldAtTheCostOfHp : SoEffet
{
    public override IEnumerator Effet(Context context, OutputPort port, List<int> amount, int tour)
    {
        port.piecePlayed.PiecePlayedUp();
        BoardPiece piece = port.thisBoardPiece;
        //yield return piece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.classic,null);

        for (int i = 0; i < context.voisins.Count; i++)
        {
            if (piece.healthPoint > amount[0])
            {
                BoardPiece voisin = context.voisins[i];
                port.thisBoardPiece = voisin;
                voisin.shield += amount[0] * 2;
                port.ThisPieceTakeDamage(amount[0]);
                port.thisBoardPiece.pieceAnimation.RefreshHealth(port.thisBoardPiece);
                yield return piece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.classic, null);
                yield return port.thisBoardPiece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.shield, piece);
            }
            else
            {
                BoardPiece voisin = context.voisins[i];
                port.thisBoardPiece = voisin;
                voisin.shield += (piece.healthPoint - 1) * 2;
                port.ThisPieceTakeDamage(amount[0]);
                port.thisBoardPiece.pieceAnimation.RefreshHealth(port.thisBoardPiece);
                yield return piece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.classic, null);
                yield return port.thisBoardPiece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.shield, piece);
            }
        }
        port.thisBoardPiece = piece;
        context.NbrDeRepetition += 1;
    }
    public override IEnumerator RepeatEffet(Context context, OutputPort port, List<int> amount, int tour, BoardPiece declencheur)
    {
        port.piecePlayed.RepeatedPieceUp();
        BoardPiece piece = port.thisBoardPiece;

        yield return piece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.classic, declencheur);

        for (int i = 0; i < context.voisins.Count; i++)
        {
            if (piece.healthPoint > amount[0])
            {
                BoardPiece voisin = context.voisins[i];
                port.thisBoardPiece = voisin;
                voisin.shield += amount[0] * 2;
                port.ThisPieceTakeDamage(amount[0]);
                port.thisBoardPiece.pieceAnimation.RefreshHealth(port.thisBoardPiece);
                if (i != 0) yield return piece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.classic, null);
                yield return port.thisBoardPiece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.shield, piece);
            }
            else
            {
                BoardPiece voisin = context.voisins[i];
                port.thisBoardPiece = voisin;
                voisin.shield += amount[0] * 2;
                voisin.shield += (piece.healthPoint - 1) * 2;
                port.ThisPieceTakeDamage(amount[0]);
                port.thisBoardPiece.pieceAnimation.RefreshHealth(port.thisBoardPiece);
                port.thisBoardPiece.pieceAnimation.RefreshHealth(port.thisBoardPiece);
                if (i != 0) yield return piece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.classic, null);
                yield return port.thisBoardPiece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.shield, piece);
            }
            
        }
        port.thisBoardPiece = piece;
        context.NbrDeRepetition += 1;
    }
}

