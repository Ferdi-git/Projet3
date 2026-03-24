using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/effetRepeatAround")]
public class SoEffetRepeatAround : SoEffet
{
    public override IEnumerator Effet(Context context,OutputPort port, List<int> test, int tour)
    {
        port.piecePlayed.PiecePlayedUp();
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPiecePlayed());
        BoardPiece piece = port.thisBoardPiece;
        for (int i = 0; i < context.voisins.Count; i++)
        {
            BoardPiece voisin = context.voisins[i];
            port.thisBoardPiece = voisin;
            yield return voisin.soPieces.pieceEffet.effet.RepeatEffet(voisin.context, port, voisin.soPieces.EfectValues, tour);
        }
        port.thisBoardPiece = piece;



    }
    public override IEnumerator RepeatEffet(Context context, OutputPort port, List<int> amount, int tour)
    {
        port.piecePlayed.RepeatedPieceUp();
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPieceRepeated());
        // faire que ça repete tout sauf les repeteur
        yield return null;
        Debug.Log("pour l'instant ne fait rien ");
    }

}
