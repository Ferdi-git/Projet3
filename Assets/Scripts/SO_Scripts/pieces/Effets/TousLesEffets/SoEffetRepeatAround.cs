using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/effetRepeatAround")]
public class SoEffetRepeatAround : SoEffet
{
    public override IEnumerator Effet(Context context,OutputPort port, List<int> test)
    {
        //Debug.Log("Request ");
        //port.EndRepeatEffetRequest(context.voisins.Count);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(2);
        BoardPiece piece = port.thisBoardPiece;
        for (int i = 0; i < context.voisins.Count; i++)
        {
            BoardPiece voisin = context.voisins[i];
            port.thisBoardPiece = voisin;
            
            //voisin.piecePersonality.PlayRepeatAnimations(i, (i * 0.2f)+0.2f);
            yield return voisin.soPieces.pieceEffet.effet.RepeatEffet(voisin.context, port, voisin.soPieces.EfectValues);
        }
        port.thisBoardPiece = piece;




    }
    public override IEnumerator RepeatEffet(Context context, OutputPort port, List<int> amount)
    {
        // faire que ça repete tout sauf les repeteur
        yield return null;
        Debug.Log("pour l'instant ne fait rien ");
    }

}
