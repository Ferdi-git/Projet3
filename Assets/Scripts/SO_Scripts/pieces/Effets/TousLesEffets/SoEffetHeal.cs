using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Effet/Heal")]
public class SoEffetHeal : SoEffet
{
    public override IEnumerator Effet(Context context,OutputPort port, List<int> amount, int tour)
    {
        port.piecePlayed.PiecePlayedUp();
        port.Heal(amount[0]);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPiecePlayed());
    }

    public override IEnumerator RepeatEffet(Context context, OutputPort port, List<int> amount, int tour)
    {
        port.piecePlayed.RepeatedPieceUp();
        port.Heal(amount[0]);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPieceRepeated());
    }
}
