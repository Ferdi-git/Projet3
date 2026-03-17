using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/effetRepeatAround")]
public class SoEffetRepeatAround : SoEffet
{
    public override void Effet(Context context,OutputPort port, List<int> test)
    {
        for (int i = 0; i < context.voisins.Count; i++)
        {
            BoardPiece voisin = context.voisins[i];
            
            voisin.piecePersonality.PlayRepeatAnimations(i, (i * 0.2f)+0.2f);
            voisin.soPieces.pieceEffet.effet.Effet(voisin.context, port, voisin.soPieces.EfectValues);
        }
        port.EndEffet();

        
    }
    
}
